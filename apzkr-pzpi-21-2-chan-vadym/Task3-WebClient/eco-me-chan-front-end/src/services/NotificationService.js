// src/services/NotificationService.js
import apiInstance from './ApiService';

export const getAllNotifications = async () => {
    try {
        const response = await apiInstance.get('/api/Notification');
        return response.data;
    } catch (error) {
        console.error('Error fetching notifications:', error);
        throw error;
    }
};