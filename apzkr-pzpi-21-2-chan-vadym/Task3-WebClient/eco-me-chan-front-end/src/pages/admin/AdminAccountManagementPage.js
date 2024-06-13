// src/pages/admin/AdminAccountManagementPage.js
import React, { useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { getAllUsers, deleteUser, updateUser } from '../../services/UserService';
import Role from '../../enums/Role';
import { Link } from 'react-router-dom';

const AdminAccountManagementPage = () => {
    const [users, setUsers] = useState([]);
    const [editingUser, setEditingUser] = useState(null);
    const [sortColumn, setSortColumn] = useState(null);
    const [sortOrder, setSortOrder] = useState('asc');
    const intl = useIntl();

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const usersData = await getAllUsers();
            setUsers(usersData);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    const getRoleName = (role) => {
        switch (role) {
            case Role.User:
                return intl.formatMessage({ id: 'role.user' });
            case Role.Admin:
                return intl.formatMessage({ id: 'role.admin' });
            case Role.MunicipalResourceManager:
                return intl.formatMessage({ id: 'role.municipalResourceManager' });
            default:
                return intl.formatMessage({ id: 'role.unknown' });
        }
    };

    const handleDeleteUser = async (userId) => {
        try {
            await deleteUser(userId);
            fetchUsers();
        } catch (error) {
            console.error('Error deleting user:', error);
        }
    };

    const handleEditUser = (user) => {
        setEditingUser(user);
    };

    const handleUpdateUser = async () => {
        try {
            await updateUser(editingUser.id, editingUser);
            setEditingUser(null);
            fetchUsers();
        } catch (error) {
            console.error('Error updating user:', error);
        }
    };

    const handleCancelEdit = () => {
        setEditingUser(null);
    };

    const handleSort = (column) => {
        if (column === sortColumn) {
            setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
        } else {
            setSortColumn(column);
            setSortOrder('asc');
        }
    };

    const sortedUsers = useMemo(() => {
        if (!sortColumn) return users;

        return [...users].sort((a, b) => {
            if (a[sortColumn] < b[sortColumn]) return sortOrder === 'asc' ? -1 : 1;
            if (a[sortColumn] > b[sortColumn]) return sortOrder === 'asc' ? 1 : -1;
            return 0;
        });
    }, [users, sortColumn, sortOrder]);

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminAccountManagementPage.title"
                        defaultMessage="Account Management"
                    />
                </h2>
                <Link to="/admin/create-user" className="btn btn-primary mb-3">
                    <FormattedMessage
                        id="adminAccountManagementPage.createUser"
                        defaultMessage="Create User"
                    />
                </Link>
                <table className="table">
                    <thead>
                    <tr>
                        <th onClick={() => handleSort('id')}>
                            ID {sortColumn === 'id' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('login')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.login"
                                defaultMessage="Login"
                            />
                            {sortColumn === 'login' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('firstName')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.firstName"
                                defaultMessage="First Name"
                            />
                            {sortColumn === 'firstName' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('lastName')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.lastName"
                                defaultMessage="Last Name"
                            />
                            {sortColumn === 'lastName' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('middleName')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.middleName"
                                defaultMessage="Middle Name"
                            />
                            {sortColumn === 'middleName' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('email')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.email"
                                defaultMessage="Email"
                            />
                            {sortColumn === 'email' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('role')}>
                            <FormattedMessage
                                id="adminAccountManagementPage.role"
                                defaultMessage="Role"
                            />
                            {sortColumn === 'role' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th>
                            <FormattedMessage
                                id="adminAccountManagementPage.edit"
                                defaultMessage="Edit"
                            />
                        </th>
                        <th>
                            <FormattedMessage
                                id="adminAccountManagementPage.delete"
                                defaultMessage="Delete"
                            />
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {sortedUsers.map((user) => (
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <input
                                        type="text"
                                        value={editingUser.login}
                                        onChange={(e) =>
                                            setEditingUser({ ...editingUser, login: e.target.value })
                                        }
                                    />
                                ) : (
                                    user.login
                                )}
                            </td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <input
                                        type="text"
                                        value={editingUser.firstName}
                                        onChange={(e) =>
                                            setEditingUser({ ...editingUser, firstName: e.target.value })
                                        }
                                    />
                                ) : (
                                    user.firstName
                                )}
                            </td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <input
                                        type="text"
                                        value={editingUser.lastName}
                                        onChange={(e) =>
                                            setEditingUser({ ...editingUser, lastName: e.target.value })
                                        }
                                    />
                                ) : (
                                    user.lastName
                                )}
                            </td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <input
                                        type="text"
                                        value={editingUser.middleName}
                                        onChange={(e) =>
                                            setEditingUser({ ...editingUser, middleName: e.target.value })
                                        }
                                    />
                                ) : (
                                    user.middleName
                                )}
                            </td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <input
                                        type="email"
                                        value={editingUser.email}
                                        onChange={(e) =>
                                            setEditingUser({ ...editingUser, email: e.target.value })
                                        }
                                    />
                                ) : (
                                    user.email
                                )}
                            </td>
                            <td>{getRoleName(user.role)}</td>
                            <td>
                                {editingUser && editingUser.id === user.id ? (
                                    <>
                                        <button
                                            className="btn btn-primary btn-sm me-2"
                                            onClick={handleUpdateUser}
                                        >
                                            <FormattedMessage
                                                id="adminAccountManagementPage.save"
                                                defaultMessage="Save"
                                            />
                                        </button>
                                        <button
                                            className="btn btn-secondary btn-sm"
                                            onClick={handleCancelEdit}
                                        >
                                            <FormattedMessage
                                                id="adminAccountManagementPage.cancel"
                                                defaultMessage="Cancel"
                                            />
                                        </button>
                                    </>
                                ) : (
                                    <button
                                        className="btn btn-primary btn-sm"
                                        onClick={() => handleEditUser(user)}
                                    >
                                        <FormattedMessage
                                            id="adminAccountManagementPage.edit"
                                            defaultMessage="Edit"
                                        />
                                    </button>
                                )}
                            </td>
                            <td>
                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => handleDeleteUser(user.id)}
                                >
                                    <FormattedMessage
                                        id="adminAccountManagementPage.delete"
                                        defaultMessage="Delete"
                                    />
                                </button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </main>
    );
};

export default AdminAccountManagementPage;