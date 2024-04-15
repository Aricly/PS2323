import { ResponsiveLine } from "@nivo/line";
import { useTheme } from "@mui/material";
import { tokens } from "../theme";
//import { mockLineData as data } from "../data/mockData";

const LineChart = ({ isCustomLineColors = false, isDashboard = false, priceRecords }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    if (!priceRecords || priceRecords === undefined) { return <p>Loading or no data available...</p>; }

    // Function to calculate the percentage change
    const calculatePercentageChange = (currentPrice, previousPrice) => {
        if (previousPrice === 0) {
            return 0; // Handle the case where there's no previous price (avoid division by zero)
        }
        return ((currentPrice - previousPrice) / previousPrice);
    };

    // Sort the price records by date in ascending order
    const sortedPriceRecords = priceRecords.sort((a, b) => new Date(a.date) - new Date(b.date));

    // Calculate the earliest and latest dates
    const earliestDate = sortedPriceRecords.length > 0 ? new Date(sortedPriceRecords[0].date) : null;
    const latestDate = sortedPriceRecords.length > 0 ? new Date(sortedPriceRecords[sortedPriceRecords.length - 1].date) : null;
    
    // Remove a month from the earliestDate
    if (earliestDate) { earliestDate.setMonth(earliestDate.getMonth() - 1); }
    // Add a month to the latestDate
    if (latestDate) { latestDate.setMonth(latestDate.getMonth() + 1); }

    // Create an array of data points with monthly average prices
    const monthlyData = [];
    let monthlySum = 0;
    let monthlyCount = 0;
    let previousMonth = null;
    let currentYear = null;
    
    sortedPriceRecords.forEach((record) => {
        const currentDate = new Date(record.date);
        const currentMonth = currentDate.getMonth();
        const currentYearValue = currentDate.getFullYear();
    
        if (previousMonth !== null && previousMonth !== currentMonth) {
            // Calculate the monthly average and add it to the data
            const monthlyAverage = monthlySum / monthlyCount;
            const date = new Date(currentYear, previousMonth, 1);
            monthlyData.push({ x: date, y: monthlyAverage });
    
            // Reset counters for the next month
            monthlySum = 0;
            monthlyCount = 0;
        }
        if (currentYear !== currentYearValue) {
            currentYear = currentYearValue;
        }
        monthlySum += record.price;
        monthlyCount += 1;
    
        previousMonth = currentMonth;
    });
    
    // Add the last monthly average data point
    if (monthlyCount > 0) {
        const lastDate = new Date(currentYear, previousMonth, 1);
        const lastMonthlyAverage = monthlySum / monthlyCount;
        monthlyData.push({ x: lastDate, y: lastMonthlyAverage });
    

    // Create an array of data points with the percentage change
    const percentageChangeData = monthlyData.map((dataPoint, index) => {
        if (index === 0) {
          return { x: dataPoint.x, y: 0 }; // No percentage change for the first data point
        } else {
            const currentPrice = dataPoint.y;
            const previousPrice = monthlyData[index - 1].y;
            const percentageChange = calculatePercentageChange(currentPrice, previousPrice);
            return { x: dataPoint.x, y: percentageChange };
        }
    });

    const formatPercentage = (value) => `${(value * 100).toFixed(2)}%`;

    return (
        <div style={{ height: '450px', width: '100%'}}>
            <ResponsiveLine
                data={[
                    {
                        id: 'Average Monthly Percentage Change',
                        data: percentageChangeData,
                    },
                ]}
                margin={{ top: 50, right: 60, bottom: 50, left: 60 }}
                xScale={{ type: 'time', format: '%Y-%m', min: earliestDate, max: latestDate}}
                xFormat="time:%Y-%m"
                yScale={{ type: 'linear', min: 'auto', max: 'auto', stacked: false, reverse: false }}
                axisTop={null}
                axisRight={null}
                axisBottom={{
                    format: '%b, %Y', // Format for date on the x-axis
                    tickValues: 'every 2 months',
                }}
                axisLeft={{
                    format: '.2%', // Format for percentage on the y-axis
                }}
                enableGridX={false}
                enableGridY
                pointSize={10}
                pointBorderWidth={2}
                useMesh
                curve='monotoneX'
                enableArea
                markers={[{
                    axis: 'y',
                    lineStyle: {
                        stroke: '#b0413e',
                        strokeWidth: 2,
                    },
                    value: 0,
                }]}
                
                tooltip={({ point }) => {
                    // Custom tooltip formatting
                    const tooltipStyle = {
                        border: '1px solid #ccc',   // Add border
                        background: 'white',        // Set background color
                        padding: '8px',             // Add padding for spacing
                        borderRadius: '5px',        // Add rounded corners
                    };

                    return (
                        <div style={tooltipStyle}>
                            <strong>{point.data.xFormatted}</strong>
                            <br />
                            { formatPercentage(point.data.y) }
                        </div>
                    );
                }}
            />
        </div>
    );
};

export default LineChart;
