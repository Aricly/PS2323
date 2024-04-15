import React from 'react';
import './LoginComponent.css';
import { useNavigate } from 'react-router-dom';

import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';

const SignUp = () => {
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();
        navigate('/dashboard');
    };

    return (
        <div className="login-container">
            <div className="content-wrapper">
                {/* Greeting section */}
                <div className="greeting-section">
                    <h1>Welcome to BuiltSight</h1>
                    <h2>YOUR GATEWAY TO BETTER CONSTRUCTION INSIGHTS</h2>
                </div>

                {/* Form section */}
                <div className="form-section">
                    <div className="logo-section">
                        <h2>CREATE ACCOUNT</h2>
                        <h3>It's free and easy</h3>
                    </div>

                    {/* Login form */}
                    <form className="login-form" onSubmit={handleLogin}>
                        <div className="input-group">
                            <label htmlFor="fname">Full name</label>
                            <input type="text" id="fname" name="fname" required />
                        </div>
                        <div className="input-group">
                            <label htmlFor="username">Email or phone number</label>
                            <input type="text" id="username" name="username" required />
                        </div>
                        <div className="input-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" id="password" name="password" required />
                        </div>

                        <FormGroup>
                            <FormControlLabel control={<Checkbox />} label="By creating an account means you agree to the Terms and Conditions, and our Privacy Policy" />
                        </FormGroup>


                        <button type="submit" className="login-button">SIGN UP</button>

                        <div className="signin-link">
                            Already have an account? <a href="/">Sign In</a>
                        </div>
                    </form>

                </div>
            </div>

            {/* Footer section */}
            <footer className="login-footer">
                BuiltSight 2023.
            </footer>
        </div>
    );
}

export default SignUp;
