// src/services/UserService.js
import apiInstance from './ApiService';

export const getAllUsers = async () => {
    try {
        const response = await apiInstance.get('/api/User');
        return response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
        throw error;
    }
};

export const deleteUser = async (userId) => {
    try {
        await apiInstance.delete(`/api/User/${userId}`);
    } catch (error) {
        console.error('Error deleting user:', error);
        throw error;
    }
};

export const updateUser = async (userId, userData) => {
    try {
        const response = await apiInstance.put(`/api/User/${userId}`, userData);
        console.log("new user", response.data);
        return response.data;
    } catch (error) {
        console.error('Error updating user:', error);
        throw error;
    }
};


export const createUser = async (userData) => {
    try {
        const response = await apiInstance.post('/api/User', userData);
        return response.data;
    } catch (error) {
        console.error('Error creating user:', error);
        throw error;
    }
};