// Registration.js
import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FormattedMessage } from 'react-intl';
import '../styles/registration.css';
import { AuthContext } from '../contexts/AuthContext';
import { register } from '../services/AuthService';
import User from '../models/User';
import Role from '../enums/Role';

const Registration = () => {
    const [userData, setUserData] = useState(new User('', '', '', '', '', '', '', Role.User));
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setUserData({ ...userData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const registeredUser = await register(userData);
            login(registeredUser);
            navigate('/login');
        } catch (error) {
            console.error('Registration failed:', error);
        }
    };

    return (
        <main className="main py-5">
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-md-6">
                        <h2>
                            <FormattedMessage id="registration.title" defaultMessage="Registration" />
                        </h2>
                        <form onSubmit={handleSubmit}>
                            <div className="mb-3">
                                <label htmlFor="login" className="form-label">
                                    <FormattedMessage id="registration.login" defaultMessage="Login" />
                                </label>
                                <input type="text" className="form-control" id="login" name="login" value={userData.login} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="password" className="form-label">
                                    <FormattedMessage id="registration.password" defaultMessage="Password" />
                                </label>
                                <input type="password" className="form-control" id="password" name="password" value={userData.password} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="phone" className="form-label">
                                    <FormattedMessage id="registration.phone" defaultMessage="Phone" />
                                </label>
                                <input type="tel" className="form-control" id="phone" name="phone" value={userData.phone} onChange={handleChange} />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="email" className="form-label">
                                    <FormattedMessage id="registration.email" defaultMessage="Email" />
                                </label>
                                <input type="email" className="form-control" id="email" name="email" value={userData.email} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="lastName" className="form-label">
                                    <FormattedMessage id="registration.lastName" defaultMessage="Last Name" />
                                </label>
                                <input type="text" className="form-control" id="lastName" name="lastName" value={userData.lastName} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="firstName" className="form-label">
                                    <FormattedMessage id="registration.firstName" defaultMessage="First Name" />
                                </label>
                                <input type="text" className="form-control" id="firstName" name="firstName" value={userData.firstName} onChange={handleChange} required />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="middleName" className="form-label">
                                    <FormattedMessage id="registration.middleName" defaultMessage="Middle Name" />
                                </label>
                                <input type="text" className="form-control" id="middleName" name="middleName" value={userData.middleName} onChange={handleChange} />
                            </div>
                            <button type="submit" className="btn btn-primary">
                                <FormattedMessage id="registration.submit" defaultMessage="Register" />
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default Registration;