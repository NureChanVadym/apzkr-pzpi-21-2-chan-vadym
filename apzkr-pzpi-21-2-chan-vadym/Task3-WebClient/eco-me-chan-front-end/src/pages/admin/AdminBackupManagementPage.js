import React from 'react';
import { FormattedMessage } from 'react-intl';
import { createBackup } from '../../services/BackupService';

const AdminBackupManagementPage = () => {
    const handleCreateBackup = async () => {
        try {
            await createBackup();
        } catch (error) {
            console.error('Error creating backup:', error);
        }
    };

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminBackupManagementPage.title"
                        defaultMessage="Backup Management"
                    />
                </h2>
                <button className="btn btn-primary" onClick={handleCreateBackup}>
                    <FormattedMessage
                        id="adminBackupManagementPage.createBackup"
                        defaultMessage="Create Backup"
                    />
                </button>
            </div>
        </main>
    );
};

export default AdminBackupManagementPage;