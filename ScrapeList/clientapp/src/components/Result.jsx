import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

function createData(materialName, unit, priceChange) {
    return { materialName, unit, priceChange };
}

const rows = [
    createData('Aggregate Blue Metal 20 mm', 'mm', '+5%'),
    // You can add more rows as needed
];

export default function MaterialTable() { // Renamed the function to a more appropriate name
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="material table">
                <TableHead>
                    <TableRow>
                        <TableCell>Material name</TableCell>
                        <TableCell>Unit</TableCell>
                        <TableCell>Price change in %</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.materialName}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.materialName}
                            </TableCell>
                            <TableCell>{row.unit}</TableCell>
                            <TableCell>{row.priceChange}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
