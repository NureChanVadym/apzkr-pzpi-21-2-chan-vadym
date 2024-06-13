// Login.js
import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FormattedMessage } from 'react-intl';
import '../styles/login.css';
import { AuthContext } from '../contexts/AuthContext';
import { login as loginUser } from '../services/AuthService';
import LoginCredentials from '../models/LoginCredentials';
import Role from '../enums/Role';

const Login = () => {
    const [credentials, setCredentials] = useState(new LoginCredentials('', ''));
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setCredentials({ ...credentials, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const userData = await loginUser(credentials);
            if (userData) {
                login(userData);
                localStorage.setItem('user', JSON.stringify(userData));
                switch (userData.role) {
                    case Role.User:
                        navigate('/user');
                        break;
                    case Role.Admin:
                        navigate('/admin');
                        break;
                    case Role.MunicipalResourceManager:
                    default:
                        navigate('/');
                }
                window.location.reload();
            } else {
                console.error('Invalid user data:', userData);
            }
        } catch (error) {
            console.error('Login failed:', error);
        }
    };

    return (
        <main className="main py-5">
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-md-6">
                        <h2>
                            <FormattedMessage id="login.title" defaultMessage="Login" />
                        </h2>
                        <form onSubmit={handleSubmit}>
                            <div className="mb-3">
                                <label htmlFor="login" className="form-label">
                                    <FormattedMessage id="login.title" defaultMessage="Login" />
                                </label>
                                <input type="text" className="form-control" id="login" name="login" value={credentials.login} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="password" className="form-label">
                                    <FormattedMessage id="login.password" defaultMessage="Password" />
                                </label>
                                <input type="password" className="form-control" id="password" name="password" value={credentials.password} onChange={handleChange} required />
                            </div>
                            <button type="submit" className="btn btn-primary">
                                <FormattedMessage id="login.submit" defaultMessage="Login" />
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default Login;