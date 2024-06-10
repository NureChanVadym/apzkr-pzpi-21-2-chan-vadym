// src/services/BackupService.js
import apiInstance from './ApiService';

export const createBackup = async () => {
    try {
        const response = await apiInstance.get('/api/Backup');
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'backup.sql');
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    } catch (error) {
        console.error('Error creating backup:', error);
        throw error;
    }
};