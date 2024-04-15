import { Box, Button, IconButton, Typography, useTheme } from "@mui/material";
import { tokens } from "../../theme";
import { mockTransactions } from "../../data/mockData";
import DownloadOutlinedIcon from "@mui/icons-material/DownloadOutlined";
import EmailIcon from "@mui/icons-material/Email";
import PointOfSaleIcon from "@mui/icons-material/PointOfSale";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import TrafficIcon from "@mui/icons-material/Traffic";
import Header from "../../components/Header";
import LineChart from "../../components/LineChart";
//ready import line chart for projection
import Dropdown from "../../components/Dropdown";
import Result from "../../components/Result";
import History from "../../components/History";
import CombinedLineChart from "../../components/CombinedLineChart";

const Dashboard = () => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Box m="20px">

            {/* HEADER */}
            <Header title="DASHBOARD" subtitle="AUSTRALIA'S CONSTRUCTION MATERIAL PRICE CHANGE PERCENTAGE TRACKER" />

            {/* Wrappers */}
            <Box display="flex" flexDirection="row" justifyContent="space-between">

                {/* Wrapper 1: Dropdown */}
                <Box flex="2" marginBottom="20px" marginRight="40px"> {/* Added marginRight for padding */}
                    <Dropdown isDashboard={true} />
                </Box>

                {/* Wrapper 2: History and Projection */}
                <Box flex="1" display="flex" flexDirection="column">

                    {/* History Panel */}
                    <Box
                        marginBottom="20px"
                        paddingLeft="20px"
                        paddingRight="20px"
                        paddingTop="30px"
                        backgroundColor={colors.primary[100]}
                        height="200px"
                    >
                        <Typography variant="h4" fontWeight="600" color={colors.grey[100]}>
                            History
                        </Typography>
                        <History isDashboard={true} />
                    </Box>

                    {/* Projection Panel */}
                    <Box flex="1" backgroundColor={colors.primary[400]} mt={2}>
                        <Box
                            mt="25px"
                            p="0 30px"
                            display="flex"
                            justifyContent="space-between"
                            alignItems="center"

                        >
                            <Box>
                                <Typography variant="h4" fontWeight="600" color={colors.grey[100]}>
                                    PROJETED PRICE CHANGE
                                </Typography>
                                <Typography variant="h6" fontWeight="bold" color={colors.greenAccent[500]}>
                                    Selected material
                                </Typography>
                            </Box>
                        </Box>
                        <Box height="250px" mt="-20px">
                            <LineChart isDashboard={true} />
                        </Box>
                    </Box>

                </Box>
            </Box>
        </Box>
    );
};

export default Dashboard;
