import React, { useState } from 'react';
import './LoginComponent.css';
import { useNavigate } from 'react-router-dom';

const LoginComponent = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();

        if (!validateEmail(email)) {
            setError('Invalid email format.');
            return;
        }

        if (password.length < 6) {
            setError('Password should be at least 6 characters long.');
            return;
        }

        if (email === 'ps2323@gmail.com' && password === 'gdaymate') {  //login email and password
            navigate('/dashboard');
        } else {
            setError('Invalid credentials.');
        }
    };

    const validateEmail = (email) => {
        const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return pattern.test(email);
    };

    return (
        <div className="login-container">
            <div className="content-wrapper">
                <div className="greeting-section">
                    <h1>Welcome to BuiltSight</h1>
                    <h2>YOUR GATEWAY TO BETTER CONSTRUCTION INSIGHTS</h2>
                </div>

                <div className="form-section">
                    <div className="logo-section">
                        <h2>WELCOME BACK</h2>
                        <h3>Let's get started!</h3>
                    </div>

                    {error && (
                        <div className="error-message">{error}</div>
                    )}

                    <form className="login-form" onSubmit={handleLogin}>
                        <div className="input-group">
                            <label htmlFor="username">Email or phone number</label>
                            <input
                                type="text"
                                id="username"
                                name="username"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                id="password"
                                name="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                        <button type="submit" className="login-button">SIGN IN</button>
                    </form>

                    <div className="signup-link">
                        Don't have an account? <a href="/signup">Sign Up</a>
                    </div>
                </div>
            </div>

            <footer className="login-footer">
                BuiltSight 2023.
            </footer>
        </div>
    );
}

export default LoginComponent;
