// src/services/IoTDeviceService.js
import apiInstance from './ApiService';

export const getUserIoTDevices = async (userId) => {
    try {
        const response = await apiInstance.get(`/api/IoTDevice/user/${userId}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching user IoT devices:', error);
        throw error;
    }
};

export const getAllIoTDevices = async () => {
    try {
        const response = await apiInstance.get('/api/IoTDevice');
        return response.data;
    } catch (error) {
        console.error('Error fetching IoT devices:', error);
        throw error;
    }
};

export const deleteIoTDevice = async (deviceId) => {
    try {
        await apiInstance.delete(`/api/IoTDevice/${deviceId}`);
    } catch (error) {
        console.error('Error deleting IoT device:', error);
        throw error;
    }
};

export const updateIoTDevice = async (deviceId, deviceData) => {
    try {
        const response = await apiInstance.put(`/api/IoTDevice/${deviceId}`, deviceData);
        return response.data;
    } catch (error) {
        console.error('Error updating IoT device:', error);
        throw error;
    }
};