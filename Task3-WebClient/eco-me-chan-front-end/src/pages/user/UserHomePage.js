// src/pages/user/UserHomePage.js

import React from 'react';
import { FormattedMessage } from 'react-intl';

const UserHomePage = () => {
    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="userHomePage.welcome"
                        defaultMessage="Welcome, User!"
                    />
                </h2>
                {/* Add user-specific content */}
            </div>
        </main>
    );
};

export default UserHomePage;