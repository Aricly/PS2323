import React from 'react';

const CreateAccountComponent = () => {
    return (
        <div>
            <h1>Create Account</h1>
            <form>
                <label>
                    Username:
                    <input type="text" name="username" />
                </label>
                <br />
                <label>
                    Password:
                    <input type="password" name="password" />
                </label>
                <br />
                <input type="submit" value="Create Account" />
            </form>
        </div>
    );
}

export default CreateAccountComponent;
