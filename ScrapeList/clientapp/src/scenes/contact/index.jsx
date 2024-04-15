import { Box, useTheme, TextField, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
import Header from "../../components/Header";
import { tokens } from "../../theme";
import React, { useState } from "react";
import LocalPhoneOutlinedIcon from '@mui/icons-material/LocalPhoneOutlined';
import EmailOutlinedIcon from '@mui/icons-material/EmailOutlined';
import FmdGoodOutlinedIcon from '@mui/icons-material/FmdGoodOutlined';

const bull = (
    <Box
        component="span"
        sx={{ display: 'inline-block', mx: '2px', transform: 'scale(0.8)' }}
    >
        •
    </Box>
);

function BasicCard() {
    return (
        <Card sx={{ height: "68%", width: "30%", minWidth: 275, maxWidth: 800, m: 2 }}>
            <CardContent>
                <Typography sx={{ fontSize: 20 }} color="text.secondary" gutterBottom>
                    CONTACT US
                </Typography>
                <Typography component="div">
                    Say Something.
                </Typography>
                <br></br>
                <br></br>
                <Box display="flex">
                    <LocalPhoneOutlinedIcon />
                    <Typography component="div" sx={{ paddingLeft: '8px' }}>
                        +1300 897 669
                    </Typography>
                </Box>
                <br></br>
                <Box display="flex">
                    <EmailOutlinedIcon />
                    <Typography component="div" sx={{ paddingLeft: '8px' }}>
                        builtsight@gmail.com
                    </Typography>
                </Box>
                <br></br>
                <Box display="flex">
                    <FmdGoodOutlinedIcon />
                    <Typography component="div" sx={{ paddingLeft: '8px' }}>
                        Parramatta NSW 2116
                    </Typography>
                </Box>
                
                
            </CardContent>
            
        </Card>
    );
}

function ContactForm() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        //
    };

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                alignItems: "left",
                width:"40%",
                m: 2,
                p: 2
            }}
        >
            <Typography align="left">
                Name
            </Typography>
            <form onSubmit={handleSubmit}>
                <TextField
                    fullWidth
                    label="Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    margin="normal"
                    required
                />
          
                <Typography align="left">
                    Email
                </Typography>
                <TextField
                    fullWidth
                    label="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    margin="normal"
                    required
                    type="email"
                />
                <Typography align="left">
                    Message
                </Typography>
                <TextField
                    fullWidth
                    label="Message"
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    margin="normal"
                    required
                    multiline
                    rows={4}
                />
                <Button
                    fullWidth
                    type="submit"
                    sx={{
                        mt: 2,
                        backgroundColor: "#000",
                        color: "#fff",
                        "&:hover": {
                            backgroundColor: "#111",
                        },
                    }}
                >
                    Submit
                </Button>
            </form>
        </Box>
    );
}

function CONTACT() {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Box m="20px">
            <Header title="CONTACT" subtitle="AUSTRALIA'S CONSTRUCTION MATERIAL PRICE CHANGE PERCENTAGE TRACKER" />

            <Box
                sx={{
                    display: "flex",
                    alignItems: "stretch",  // change to stretch to make children take full height
                    justifyContent: "center",
                    height: "80vh",  // reduced the height to make space for the header
                }}
            >
                <BasicCard />
                <ContactForm />
            </Box>
        </Box>
    );
}

export default CONTACT;