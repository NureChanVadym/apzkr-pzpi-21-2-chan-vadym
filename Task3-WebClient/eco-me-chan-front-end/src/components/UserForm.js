// src/components/UserForm.js
import React from 'react';
import { FormattedMessage } from 'react-intl';

const UserForm = ({ user, handleChange }) => {
    return (
        <>
            <div className="mb-3">
                <label htmlFor="login" className="form-label">
                    <FormattedMessage id="userForm.login" defaultMessage="Login" />
                </label>
                <input
                    type="text"
                    className="form-control"
                    id="login"
                    name="login"
                    value={user.login}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="mb-3">
                <label htmlFor="email" className="form-label">
                    <FormattedMessage id="userForm.email" defaultMessage="Email" />
                </label>
                <input
                    type="email"
                    className="form-control"
                    id="email"
                    name="email"
                    value={user.email}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="mb-3">
                <label htmlFor="firstName" className="form-label">
                    <FormattedMessage id="userForm.firstName" defaultMessage="First Name" />
                </label>
                <input
                    type="text"
                    className="form-control"
                    id="firstName"
                    name="firstName"
                    value={user.firstName}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="mb-3">
                <label htmlFor="lastName" className="form-label">
                    <FormattedMessage id="userForm.lastName" defaultMessage="Last Name" />
                </label>
                <input
                    type="text"
                    className="form-control"
                    id="lastName"
                    name="lastName"
                    value={user.lastName}
                    onChange={handleChange}
                    required
                />
            </div>
            <div className="mb-3">
                <label htmlFor="middleName" className="form-label">
                    <FormattedMessage id="userForm.middleName" defaultMessage="Middle Name" />
                </label>
                <input
                    type="text"
                    className="form-control"
                    id="middleName"
                    name="middleName"
                    value={user.middleName}
                    onChange={handleChange}
                />
            </div>
            <div className="mb-3">
                <label htmlFor="phone" className="form-label">
                    <FormattedMessage id="userForm.phone" defaultMessage="Phone" />
                </label>
                <input
                    type="tel"
                    className="form-control"
                    id="phone"
                    name="phone"
                    value={user.phone}
                    onChange={handleChange}
                />
            </div>
        </>
    );
};

export default UserForm;