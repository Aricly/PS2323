// Importing necessary libraries and components
import React from 'react';
import { ColorModeContext } from "./theme"; // Replace with the correct path

// Component to check if ColorModeContext is a valid React context
const FileChecker = () => {
    // Trying to use the context
    try {
        const colorMode = React.useContext(ColorModeContext);

        // If context is valid and there are no errors, it will reach this point
        return <div>ColorModeContext is a valid React context.</div>;
    } catch (error) {
        // If there's any error, this block will execute
        return <div>Error with ColorModeContext: {error.message}</div>;
    }
};

export default FileChecker;
