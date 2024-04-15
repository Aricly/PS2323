import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Button from '@mui/material/Button';
import { useState, useEffect } from 'react';
import axios from 'axios';
import LineChart from './LineChart';

export default function BasicSelect() {
    const [category, setCategory] = React.useState('');
    const [material, setMaterial] = React.useState('');
    const [startDate, setStartDate] = React.useState('');
    const [endDate, setEndDate] = React.useState('');
    const [startDates, setStartDates] = React.useState([]); 
    const [showTable, setShowTable] = useState(false);
    const [showExport, setShowExport] = useState(false);
    


    const handleCategoryChange = (event) => {
        const selectedCategoryId = event.target.value;
        setCategory(selectedCategoryId);

        // Fetch materials for the selected category
        axios.get(`https://localhost:44306/api/dropdown/materials?categoryID=${selectedCategoryId}`)
            .then(response => {
                setMaterials(response.data.$values || []);
            })
            .catch(err => {
                console.error("Error fetching materials:", err);
            });
        setShowTable(false);
        setShowExport(false);
    };


    const handleMaterialChange = (event) => {
        const selectedMaterialID = event.target.value;
        setMaterial(selectedMaterialID);

        axios.get(`https://localhost:44306/api/data/priceRecords`, {
            params: { materialID: selectedMaterialID }
        })
            .then(response => {
                // Ensure $values is an array before attempting to map over it
                if (Array.isArray(response.data.$values)) {
                    setPriceRecords(response.data.$values);

                    // Extract unique dates from priceRecords and set them to startDates state
                    const uniqueDates = [...new Set(response.data.$values.map(record => record.date))];
                    setStartDates(uniqueDates);
                } else {
                    console.error("Unexpected response format:", response.data);
                }
            })
            .catch(err => {
                console.error('Error fetching data:', err);
            });
        setShowTable(false);
        setShowExport(false);
    }


    const handleStartDateChange = (event) => {
        setStartDate(event.target.value);
        setEndDate(''); // Reset the ending date
        setShowTable(false);
        setShowExport(false);
    };
    const handleEndDateChange = (event) => {
        setEndDate(event.target.value);
        setShowTable(false);
        setShowExport(false);

    }

    const [categories, setCategories] = useState([]);
    const [materials, setMaterials] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [priceRecords, setPriceRecords] = useState([]); // State variable for priceRecords

    useEffect(() => {
        // Fetch categories
        axios.get('https://localhost:44306/api/dropdown/categories')
            .then(response => {
                setCategories(response.data.$values);
                setIsLoading(false);
            })
            .catch(err => {
                setError(err);
                setIsLoading(false);
            });
        setShowTable(false);
        setShowExport(false);
    }, []);

    // Function to convert price records to CSV format
    const convertToCSV = () => {
        const csv = [];
        csv.push(['Date', 'Material Name', 'Price', '% Change']); // CSV header
        var percentChange = calculatePriceChanges(priceRecords, startDate, endDate);

        percentChange.forEach((record, index) => {    
            const formattedDate = new Intl.DateTimeFormat('en-GB', { month: 'long', year: 'numeric' }).format(new Date(record.date));
            const materialName = materials[0].materialName; // Assuming materials is available
    
            // Create a single array for each row with all values
            const row = [
                formattedDate,
                materialName,
                record.price,
                record.percentageChange.toFixed(2), // Limit to 2 decimal places
            ];
    
            csv.push(row);
        
            // Update previousPrice for the next iteration
        });
        
        // Flatten the array into CSV format
        const csvRows = csv.map(row => row.join(','));
        return csvRows.join('\n');
    };

    // Function to trigger CSV download
    const downloadCSV = () => {
        const csv = convertToCSV();
        const blob = new Blob([csv], { type: 'text/csv' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');

        a.href = url;
        a.download = 'price_records.csv';
        a.click();
        window.URL.revokeObjectURL(url);
    };

    const handleCompareClick = () => {
        console.log("Comparing data...");
        if (!category || !material || !startDate || !endDate)
        {
            console.log("Missing Inputs!")
        } else {
            setShowTable(true);
            setShowExport(true);
        };
    };

    const handleExportClick = () => {
        console.log("Exporting data...");
        downloadCSV();
    };



    if (isLoading) {
        return <p>Loading...</p>;
    }

    if (error) {
        return <p>Error: {error.message}</p>;
    }


    const groupRecordsByMonth = (records) => {
        const grouped = {};

        records.forEach(record => {
            const date = new Date(record.date);
            const monthYear = `${date.getMonth() + 1}-${date.getFullYear()}`; // Format: MM-YYYY

            if (!grouped[monthYear]) {
                grouped[monthYear] = [];
            }

            grouped[monthYear].push(record);
        });

        return grouped;
    };

    const calculatePriceChanges = (priceRecords, startDate, endDate) => {
        const groupedByMonth = groupRecordsByMonth(priceRecords);
        const averagedRecords = [];

        for (const monthYear in groupedByMonth) {
            const records = groupedByMonth[monthYear];
            const averagePrice = records.reduce((acc, record) => acc + record.price, 0) / records.length;

            averagedRecords.push({
                date: records[0].date,
                price: averagePrice,
                change: 0 // Placeholder, will be calculated in the next step
            });
        }

        const sortedAveragedRecords = averagedRecords.sort((a, b) => new Date(a.date) - new Date(b.date));

        // Now, calculate the price change for each averaged record
        const priceChanges = sortedAveragedRecords.map((record, index) => {
            let change = 0;
            let percentageChange = 0; // Placeholder for percentage change
            if (index > 0) {
                change = record.price - sortedAveragedRecords[index - 1].price;
                percentageChange = (change / sortedAveragedRecords[index - 1].price) * 100;
            }
            return { ...record, change, percentageChange }; // Add percentageChange to the returned record
        });

        // Finally, filter by the specified start and end dates
        return priceChanges.filter(record =>
            new Date(record.date) >= new Date(startDate) &&
            new Date(record.date) <= new Date(endDate)
        );
    };


    const priceChanges = calculatePriceChanges(priceRecords, startDate, endDate);
    return (
        <Box display="flex" flexDirection="column" gap={2}>
            {/* Category and Material Dropdowns */}
            <Box display="flex" gap={2}>
                {/* Select Category Dropdown */}
                <FormControl fullWidth sx={{ minWidth: 120 }}>
                    <InputLabel id="category-select-label">Select category</InputLabel>
                    <Select
                        labelId="category-select-label"
                        id="category-select"
                        value={category}
                        label="Select category"
                        onChange={handleCategoryChange}
                    >
                        {categories.map(c => (
                            <MenuItem key={c.categoryID} value={c.categoryID}>
                                {c.categoryName}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                {/* Select Material Dropdown */}
                <FormControl fullWidth sx={{ minWidth: 120 }}>
                    <InputLabel id="material-select-label">Select material</InputLabel>
                    <Select
                        labelId="material-select-label"
                        id="material-select"
                        value={material}
                        label="Select material"
                        onChange={handleMaterialChange}
                    >
                        {materials.map(m => (
                            <MenuItem key={m.materialID} value={m.materialID}>
                                {m.materialName}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </Box>

            {/* Starting and Ending Date Dropdowns */}
            <Box display="flex" gap={2}>
                {/* Pick Starting Date Dropdown */}
                <FormControl fullWidth sx={{ minWidth: 120 }}>
                    <InputLabel id="start-date-select-label">Pick starting date</InputLabel>
                    <Select
                        labelId="start-date-select-label"
                        id="start-date-select"
                        value={startDate}
                        label="Pick starting date"
                        onChange={handleStartDateChange}
                    >
                        {startDates.map((date, index) => (
                            <MenuItem key={index} value={date}>
                                {new Intl.DateTimeFormat('en-GB', { dateStyle:'medium' }).format(new Date(date))}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>



                {/* Pick Ending Date Dropdown */}
                <FormControl fullWidth sx={{ minWidth: 120 }}>
                    <InputLabel id="end-date-select-label">Pick ending date</InputLabel>
                    <Select
                        labelId="end-date-select-label"
                        id="end-date-select"
                        value={endDate}
                        label="Pick ending date"
                        onChange={handleEndDateChange}
                    >
                        {startDates
                            .filter(endDateOption => new Date(endDateOption) > new Date(startDate))
                            .map((date, index) => (
                                <MenuItem key={index} value={date}>
                                    {new Intl.DateTimeFormat('en-GB', { dateStyle:'medium' }).format(new Date(date))}
                                </MenuItem>
                            ))}
                    </Select>
                </FormControl>

            </Box>

            {/* COMPARE Button */}
            <Button variant="contained" color="primary" onClick={handleCompareClick}>
                COMPARE
            </Button>
            {/* EXPORT Button */}
            {showExport && (
                <Button variant="contained" color="primary" onClick={handleExportClick}>
                EXPORT
            </Button>
            )}
            
            {/* Table of Results */}
            {showTable && (
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Month</TableCell> {/* Updated from "Date" to "Month" */}
                            <TableCell>Price</TableCell>
                            <TableCell>Percentage Change</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {priceChanges.map((record, index) => (
                            <TableRow key={index}>
                                {/* Displaying only the month in "Month Year" format */}
                                <TableCell>{new Intl.DateTimeFormat('en-GB', { month: 'long', year: 'numeric' }).format(new Date(record.date))}</TableCell>

                                {/* Displaying the price to 2 decimal places */}
                                <TableCell>{record.price.toFixed(2)}</TableCell>

                                <TableCell>{record.percentageChange.toFixed(2)}%</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            )}

            {/* LineChart */}
            {showTable && (
                <Box color="primary" borderRadius={2} p={2} boxShadow={3}>
                    PERCENTAGE CHANGE IN MATERIAL PRICE
                    <LineChart priceRecords={priceRecords.filter(record => new Date(record.date) >= new Date(startDate) && new Date(record.date) <= new Date(endDate))} />
                </Box>

            )}


        </Box>
    );
}
