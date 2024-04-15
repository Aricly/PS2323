import React from 'react';
import { Button, TextField, Typography, Container, Box, CssBaseline } from '@mui/material';
import { styled } from '@mui/system';


const StyledContainer = styled(Container)({
    display: 'flex',
    flexDirection: 'row',
    height: '100vh'
});

const GreetingSection = styled(Box)({
    flex: 1,
    backgroundColor: 'midnightblue',
    color: 'white',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center'
});

const FormSection = styled(Box)({
    flex: 2,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center'
});

const LoginComponent = () => {
    console.log("Rendering LoginComponent");
    return (
        <StyledContainer>
            <CssBaseline />
            <GreetingSection>
                <Typography variant="h4">Welcomeeee to BuiltSight</Typography>
                <Typography variant="subtitle1">YOUR GATEWAY TO BETTER CONSTRUCTION INSIGHTS</Typography>
            </GreetingSection>
            <FormSection>
                <Box mb={2}>
                    <Typography variant="h6" align="left">WELCOME BACK</Typography>
                    <Typography variant="subtitle2" align="left">Let's get started!</Typography>
                </Box>
                <Box component="form" width="250px" mb={2}>
                    <TextField label="Username" variant="outlined" fullWidth margin="normal" />
                    <TextField label="Password" variant="outlined" fullWidth type="password" margin="normal" />
                    <Button variant="contained" color="primary" fullWidth size="large">LOG IN</Button>
                </Box>
                <Typography variant="body2">Don't have an account? <a href="/signup">Sign Up</a></Typography>
            </FormSection>
            <Box position="absolute" bottom={0} width="100%" bgcolor="#2a2a2a" p={2} textAlign="center">
                <Typography color="white">BuiltSight 2023</Typography>
            </Box>
        </StyledContainer>
    );
}

export default LoginComponent;
