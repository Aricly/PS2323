import { Box, useTheme, Card, CardContent, Typography } from "@mui/material";
import Header from "../../components/Header";
import { tokens } from "../../theme";

const FAQ = () => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Box m="20px">
            <Header title="ABOUT US" subtitle="AUSTRALIA'S CONSTRUCTION MATERIAL PRICE CHANGE PERCENTAGE TRACKER" />
            <br></br>
            <br></br>
            <Card sx={{ bgcolor: colors.grey[800], p: "20px", borderRadius: "8px", width: "70%", mx: "auto" }}>
                <CardContent>
                    <Typography variant="h5" mb="10px">
                        At BuiltSight, we bridge the gap between construction contractors and the ever-evolving world of material pricing.
                    </Typography>
                    <Typography>
                        Born out of a passion for innovation and a deep understanding of the construction industry,
                        our platform offers real-time insights into material price fluctuations,
                        ensuring our users are always a step ahead. By harnessing the power of cutting-edge technology and meticulous market research,
                        we've created a tool that not only tracks historical price data but also provides future price projections.
                    </Typography>
                    <Typography mt={2}>
                        Our dedicated team comprises experts from both the construction and tech domains,
                        ensuring a seamless blend of practicality and innovation.
                        Join us as we pave the way for smarter, more informed construction planning.
                    </Typography>
                </CardContent>
            </Card>
        </Box>
    );
};

export default FAQ;
