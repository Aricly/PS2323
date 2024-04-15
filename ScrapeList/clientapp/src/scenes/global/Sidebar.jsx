import { useState } from "react";
import { ProSidebar, Menu, MenuItem } from "react-pro-sidebar";
import { Box, IconButton, Typography, useTheme } from "@mui/material";
import { Link } from "react-router-dom";
import "react-pro-sidebar/dist/css/styles.css";
import { tokens } from "../../theme";
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import PeopleOutlinedIcon from "@mui/icons-material/PeopleOutlined";
import ContactsOutlinedIcon from "@mui/icons-material/ContactsOutlined";
import ReceiptOutlinedIcon from "@mui/icons-material/ReceiptOutlined";
import PersonOutlinedIcon from "@mui/icons-material/PersonOutlined";
import CalendarTodayOutlinedIcon from "@mui/icons-material/CalendarTodayOutlined";
import HelpOutlineOutlinedIcon from "@mui/icons-material/HelpOutlineOutlined";
import BarChartOutlinedIcon from "@mui/icons-material/BarChartOutlined";
import PieChartOutlineOutlinedIcon from "@mui/icons-material/PieChartOutlineOutlined";
import TimelineOutlinedIcon from "@mui/icons-material/TimelineOutlined";
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import MapOutlinedIcon from "@mui/icons-material/MapOutlined";
import LightModeOutlinedIcon from "@mui/icons-material/LightModeOutlined";
import DarkModeOutlinedIcon from "@mui/icons-material/DarkModeOutlined";
import CallEndOutlinedIcon from '@mui/icons-material/CallEndOutlined';
import LogoutOutlinedIcon from '@mui/icons-material/LogoutOutlined';
import ArrowForwardIosOutlinedIcon from '@mui/icons-material/ArrowForwardIosOutlined';
import { useLocation } from "react-router-dom";
import { useContext } from "react";
import { ColorModeContext } from "../../theme"; // Import ColorModeContext
//import Topbar from "./scenes/global/Topbar";
import { TopbarButton } from "./Topbar"; // Import TopbarButton

const handleExportClick = () => {
    // This is where we'll add our logic to talk to the backend and get the data to export
    console.log("Export Data button clicked");
};

const Item = ({ title, to, icon, selected, setSelected }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    return (
        <MenuItem
            active={selected === title}
            style={{ color: colors.grey[900], paddingBottom: '30px' }}
            onClick={() => setSelected(title)}
            icon={icon}
        >
            <Typography>{title}</Typography>
            <Link to={to} />
        </MenuItem>
    );
};


const Sidebar = () => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [isCollapsed, setIsCollapsed] = useState(false);
    const [selected, setSelected] = useState("Dashboard");
    const colorMode = useContext(ColorModeContext); 
    


    return (
        <Box
            sx={{
                height: '100%',
                flex: 1,  // <-- This will allow the Sidebar to occupy the full available space
                display: 'flex',
                flexDirection: 'column',
                "& .pro-sidebar-inner": {
                    background: `${colors.primary[600]} !important`,
                },
                "& .pro-icon-wrapper": {
                    backgroundColor: "transparent !important",
                },
                "& .pro-inner-item": {
                    padding: "0px 0px 0px 20px !important", //spacing between buttons
                },
                "& .pro-inner-item:hover": {
                    color: "#868dfb !important",
                },
                "& .pro-menu-item.active": {
                    color: "#6870fa !important",
                },
            }}
            
            
        >
            
            <ProSidebar collapsed={isCollapsed} style={{ height: '100%' }}>
                <Menu iconShape="square">
                    {/* LOGO AND MENU ICON */}
                    <MenuItem
                        onClick={() => setIsCollapsed(!isCollapsed)}
                        icon={isCollapsed ? <MenuOutlinedIcon /> : undefined}
                        style={{
                            margin: "10px 0 0px 0",
                            color: colors.grey[100],
                        }}
                    >
                        {!isCollapsed && (
                            <Box
                                display="flex"
                                justifyContent="left"
                                alignItems="left"
                                ml="15px"
                            >
                                
                                {/*<IconButton onClick={() => setIsCollapsed(!isCollapsed)}>
                                    <MenuOutlinedIcon />
                                </IconButton>*/}
                            </Box>
                        )}
                    </MenuItem>

                    {!isCollapsed && (
                        <Box mb="280px"> {/* space between icon and buttons */}
                            <Box display="flex" justifyContent="left" alignItems="left">
                                <img
                                    alt="profile-user"
                                    width="85px"
                                    height="70px"
                                    src={`../../assets/user.png`}
                                    style={{ cursor: "pointer", borderRadius: "50%", paddingLeft: "15px" }}
                                />
                                <Typography
                                    variant="h4"
                                    color={colors.grey[900]}
                                    fontWeight="bold"
                                    sx={{ m: "25px 0 0 0", paddingLeft: "15px" }}
                                >
                                    User, hello!
                                </Typography>
                            </Box>
                            {/*<Box display="flex" textAlign="center">
                                <Typography
                                    variant="h2"
                                    color={colors.grey[900]}
                                    fontWeight="bold"
                                    sx={{ m: "10px 0 0 0" }}
                                >
                                    User
                                </Typography>
                                <Typography variant="h5" color={colors.greenAccent[500]}>
                                    Hello!
                                </Typography>
                            </Box>*/}
                        </Box>
                    )}

                    <Box paddingLeft={isCollapsed ? undefined : "10%"}>

                        {/*<Item   //export data button, replace the handleExportClick function with the actual logic to talk to the backend and retrieve the export data.
                            title="EXPORT DATA"
                            to="#"
                            icon={<BarChartOutlinedIcon />}
                            selected={selected}
                            setSelected={handleExportClick}
                        />*/}

                        <Item
                            title="DASHBOARD"
                            to="/dashboard"
                            icon={<HomeOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />

                        <MenuItem
                            active={selected === "DAY/NIGHT MODE"}
                            onClick={() => setSelected("DAY/NIGHT MODE")}
                            style={{ color: colors.grey[900], paddingBottom: '30px' }}
                        >
                            <Box display="flex" flexDirection="row" alignItems="center">
                                <TopbarButton />
                                <Typography style={{ marginLeft: "10px" }}>DAY/NIGHT MODE</Typography>
                            </Box>
                        </MenuItem>
                        
                        {/*<Item
                            title="NIGHT MODE"
                            to="/dashboard"
                            icon={<LightModeOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />*/}

                        <Item
                            title="CONTACT"
                            to="/contact"
                            icon={<CallEndOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />


                        <Item
                            title="ABOUT"
                            to="/faq"
                            icon={<HelpOutlineOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />  
                        
                        {/*<Item
                            title="LINE CHART"
                            to="/line"
                            icon={<TimelineOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />*/}

                        <Item
                            title="LOG OUT"
                            to="/"
                            icon={<LogoutOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />
                       
                    </Box>
                </Menu>
            </ProSidebar>
        </Box>
    );
};

export default Sidebar;
