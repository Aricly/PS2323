import React, { useContext, createContext } from 'react';

// Ensure that the context is created using React's createContext
const MyContext = createContext();

const MyComponent = () => {
    // Use the useContext hook only with a valid context
    const contextValue = useContext(MyContext);

    return (
        <div>
            {/* Safely use the context value */}
            {contextValue ? contextValue.someProperty : "Default Value"}
        </div>
    );
};

const App = () => {
    return (
        // Provide a value to the context when using the context provider
        <MyContext.Provider value={{ someProperty: 'Hello from context!' }}>
            <MyComponent />
        </MyContext.Provider>
    );
};

export default App;
