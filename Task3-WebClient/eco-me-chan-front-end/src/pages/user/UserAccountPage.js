// src/pages/user/UserAccountPage.js
import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FormattedMessage } from 'react-intl';
import '../../styles/accountPage.css';
import { AuthContext } from '../../contexts/AuthContext';
import { updateUser } from '../../services/UserService';

const UserAccountPage = () => {
    const { user, setUser, logout } = useContext(AuthContext);
    const [editingUser, setEditingUser] = useState(null);
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            await logout();
            navigate('/');
            window.location.reload();
        } catch (error) {
            console.error('Logout failed:', error);
        }
    };

    const handleEditUser = () => {
        setEditingUser({ ...user });
    };

    const handleUpdateUser = async () => {
        try {
            await updateUser(editingUser.id, editingUser);

            const updatedUser = {
                ...user,
                login: editingUser.login,
                email: editingUser.email,
                firstName: editingUser.firstName,
                lastName: editingUser.lastName,
                middleName: editingUser.middleName,
                phone: editingUser.phone,
            };

            setUser(updatedUser);
            localStorage.setItem('user', JSON.stringify(updatedUser));
            setEditingUser(null);
        } catch (error) {
            console.error('Error updating user:', error);
        }
    };



    const handleCancelEdit = () => {
        setEditingUser(null);
    };

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="userAccountPage.title"
                        defaultMessage="User Account"
                    />
                </h2>
                <div className="account-info">
                    {user && (
                        <>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.login"
                                        defaultMessage="Login:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="text"
                                        value={editingUser.login}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, login: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.login
                                )}
                            </p>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.email"
                                        defaultMessage="Email:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="email"
                                        value={editingUser.email}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, email: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.email
                                )}
                            </p>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.firstName"
                                        defaultMessage="First Name:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="text"
                                        value={editingUser.firstName}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, firstName: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.firstName
                                )}
                            </p>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.lastName"
                                        defaultMessage="Last Name:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="text"
                                        value={editingUser.lastName}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, lastName: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.lastName
                                )}
                            </p>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.middleName"
                                        defaultMessage="Middle Name:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="text"
                                        value={editingUser.middleName}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, middleName: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.middleName
                                )}
                            </p>
                            <p>
                                <strong>
                                    <FormattedMessage
                                        id="userAccountPage.phoneNumber"
                                        defaultMessage="Phone Number:"
                                    />
                                </strong>{' '}
                                {editingUser ? (
                                    <input
                                        type="tel"
                                        value={editingUser.phone}
                                        onChange={(e) =>
                                            setEditingUser({...editingUser, phone: e.target.value})
                                        }
                                    />
                                ) : (
                                    user.phone
                                )}
                            </p>
                        </>
                    )}
                    <div className="btn-group">
                        {editingUser ? (
                            <>
                                <button className="btn btn-primary" onClick={handleUpdateUser}>
                                    <FormattedMessage id="userAccountPage.saveChanges" defaultMessage="Save Changes"/>
                                </button>
                                <button className="btn btn-secondary" onClick={handleCancelEdit}>
                                    <FormattedMessage id="userAccountPage.cancel" defaultMessage="Cancel"/>
                                </button>
                            </>
                        ) : (
                            <>
                                <button className="btn btn-primary" onClick={handleEditUser}>
                                    <FormattedMessage id="userAccountPage.editAccount" defaultMessage="Edit Account"/>
                                </button>
                                <button className="btn btn-danger" onClick={handleLogout}>
                                    <FormattedMessage id="accountPage.logout" defaultMessage="Logout"/>
                                </button>
                            </>
                        )}
                    </div>
                </div>
            </div>
        </main>
    );
};

export default UserAccountPage;