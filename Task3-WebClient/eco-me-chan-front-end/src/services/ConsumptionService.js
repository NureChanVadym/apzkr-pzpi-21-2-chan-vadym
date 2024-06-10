// src/services/ConsumptionService.js
import apiInstance from './ApiService';

export const getUserConsumptionHistory = async (userId) => {
    try {
        const response = await apiInstance.get(`/api/Consumption/user/${userId}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching user consumption history:', error);
        throw error;
    }
};