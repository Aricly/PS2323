import { Box, useTheme } from "@mui/material";
import { ResponsiveLine } from "@nivo/line";
import { tokens } from "../theme";
import Header from "./Header";

const CombinedLineChart = ({ isCustomLineColors = false, isDashboard = false, priceRecords }) => {

    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    const LineChart = () => {
        if (!priceRecords || priceRecords.length === 0) {
            return <p>Loading or no data available...</p>;
        }

        return (
            <ResponsiveLine
                data={priceRecords}
                theme={{
                    axis: {
                        domain: {
                            line: {
                                stroke: colors.grey[900],
                            },
                        },
                        legend: {
                            text: {
                                fill: colors.grey[900],
                            },
                        },
                        ticks: {
                            line: {
                                stroke: colors.grey[900],
                                strokeWidth: 1,
                            },
                            text: {
                                fill: colors.grey[900],
                            },
                        },
                    },
                    legends: {
                        text: {
                            fill: colors.grey[900],
                        },
                    },
                    tooltip: {
                        container: {
                            color: colors.primary[500],
                        },
                    },
                }}
                colors={isDashboard ? { datum: "color" } : { scheme: "nivo" }} // added
                margin={{ top: 50, right: 110, bottom: 50, left: 60 }}
                xScale={{ type: "point" }}
                yScale={{
                    type: "linear",
                    min: "auto",
                    max: "auto",
                    stacked: true,
                    reverse: false,
                }}
                yFormat=" >-.2f"
                curve="catmullRom"
                axisTop={null}
                axisRight={null}
                axisBottom={{
                    orient: "bottom",
                    tickSize: 0,
                    tickPadding: 5,
                    tickRotation: 0,
                    legend: isDashboard ? undefined : "Month", // added
                    legendOffset: 36,
                    legendPosition: "middle",
                }}
                axisLeft={{
                    orient: "left",
                    tickValues: 5, // added
                    tickSize: 3,
                    tickPadding: 5,
                    tickRotation: 0,
                    legend: isDashboard ? undefined : "Percentage", // added
                    legendOffset: -40,
                    legendPosition: "middle",
                }}
                enableGridX={false}
                enableGridY={false}
                pointSize={8}
                pointColor={{ theme: "background" }}
                pointBorderWidth={2}
                pointBorderColor={{ from: "serieColor" }}
                pointLabelYOffset={-12}
                useMesh={true}
                legends={[
                    {
                        anchor: "bottom-right",
                        direction: "column",
                        justify: false,
                        translateX: 100,
                        translateY: 0,
                        itemsSpacing: 0,
                        itemDirection: "left-to-right",
                        itemWidth: 80,
                        itemHeight: 20,
                        itemOpacity: 0.75,
                        symbolSize: 12,
                        symbolShape: "circle",
                        symbolBorderColor: "rgba(0, 0, 0, .5)",
                        effects: [
                            {
                                on: "hover",
                                style: {
                                    itemBackground: "rgba(0, 0, 0, .03)",
                                    itemOpacity: 1,
                                },
                            },
                        ],
                    },
                ]}
            />
        );
    };

    return (
        <Box m="20px">
            {/*<Header title="Line Chart" subtitle="Simple Line Chart" />*/}
            <Box height="100%">
                <LineChart />
            </Box>
        </Box>
    );
};

export default CombinedLineChart;
