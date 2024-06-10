import React from 'react';
import { FormattedMessage } from 'react-intl';

const AdminHomePage = () => {
    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminHomePage.welcome"
                        defaultMessage="Welcome, Admin!"
                    />
                </h2>
                {/* Add admin-specific content */}
            </div>
        </main>
    );
};

export default AdminHomePage;