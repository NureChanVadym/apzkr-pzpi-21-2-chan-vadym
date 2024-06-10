// src/pages/admin/AdminCreateUserPage.js
import React, { useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { createUser } from '../../services/UserService';
import Role from '../../enums/Role';
import { useNavigate } from 'react-router-dom';

const AdminCreateUserPage = () => {
    const [newUser, setNewUser] = useState({
        login: '',
        password: '',
        phone: '',
        email: '',
        lastName: '',
        firstName: '',
        middleName: '',
        role: Role.User,
    });
    const intl = useIntl();
    const navigate = useNavigate();

    const handleCreateUser = async () => {
        try {
            await createUser(newUser);
            navigate('/admin/account-management');
        } catch (error) {
            console.error('Error creating user:', error);
        }
    };

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminCreateUserPage.title"
                        defaultMessage="Create User"
                    />
                </h2>
                <form>
                    <div className="mb-3">
                        <label htmlFor="login" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.login" defaultMessage="Login" />
                        </label>
                        <input
                            type="text"
                            className="form-control"
                            id="login"
                            value={newUser.login}
                            onChange={(e) => setNewUser({ ...newUser, login: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="password" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.password" defaultMessage="Password" />
                        </label>
                        <input
                            type="password"
                            className="form-control"
                            id="password"
                            value={newUser.password}
                            onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="phone" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.phone" defaultMessage="Phone" />
                        </label>
                        <input
                            type="tel"
                            className="form-control"
                            id="phone"
                            value={newUser.phone}
                            onChange={(e) => setNewUser({ ...newUser, phone: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="email" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.email" defaultMessage="Email" />
                        </label>
                        <input
                            type="email"
                            className="form-control"
                            id="email"
                            value={newUser.email}
                            onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="lastName" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.lastName" defaultMessage="Last Name" />
                        </label>
                        <input
                            type="text"
                            className="form-control"
                            id="lastName"
                            value={newUser.lastName}
                            onChange={(e) => setNewUser({ ...newUser, lastName: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="firstName" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.firstName" defaultMessage="First Name" />
                        </label>
                        <input
                            type="text"
                            className="form-control"
                            id="firstName"
                            value={newUser.firstName}
                            onChange={(e) => setNewUser({ ...newUser, firstName: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="middleName" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.middleName" defaultMessage="Middle Name" />
                        </label>
                        <input
                            type="text"
                            className="form-control"
                            id="middleName"
                            value={newUser.middleName}
                            onChange={(e) => setNewUser({ ...newUser, middleName: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="role" className="form-label">
                            <FormattedMessage id="adminCreateUserPage.role" defaultMessage="Role" />
                        </label>
                        <select
                            className="form-select"
                            id="role"
                            value={newUser.role}
                            onChange={(e) => setNewUser({ ...newUser, role: parseInt(e.target.value) })}
                        >
                            <option value={Role.User}>
                                {intl.formatMessage({ id: 'role.user' })}
                            </option>
                            <option value={Role.MunicipalResourceManager}>
                                {intl.formatMessage({ id: 'role.municipalResourceManager' })}
                            </option>
                        </select>
                    </div>
                    <button type="button" className="btn btn-primary" onClick={handleCreateUser}>
                        <FormattedMessage id="adminCreateUserPage.createUser" defaultMessage="Create User" />
                    </button>
                </form>
            </div>
        </main>
    );
};

export default AdminCreateUserPage;