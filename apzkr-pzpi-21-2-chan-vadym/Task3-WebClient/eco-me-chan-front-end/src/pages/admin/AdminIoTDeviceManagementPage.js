// src/pages/admin/AdminIoTDeviceManagementPage.js
import React, { useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { getAllIoTDevices, deleteIoTDevice, updateIoTDevice } from '../../services/IoTDeviceService';
import { getDeviceStatus, getDeviceType } from '../../utils/DeviceUtils';

const AdminIoTDeviceManagementPage = () => {
    const [iotDevices, setIoTDevices] = useState([]);
    const [editingDevice, setEditingDevice] = useState(null);
    const [sortColumn, setSortColumn] = useState(null);
    const [sortOrder, setSortOrder] = useState('asc');
    const intl = useIntl();

    useEffect(() => {
        fetchIoTDevices();
    }, []);

    const fetchIoTDevices = async () => {
        try {
            const devicesData = await getAllIoTDevices();
            setIoTDevices(devicesData);
        } catch (error) {
            console.error('Error fetching IoT devices:', error);
        }
    };

    const handleDeleteDevice = async (deviceId) => {
        try {
            await deleteIoTDevice(deviceId);
            fetchIoTDevices();
        } catch (error) {
            console.error('Error deleting IoT device:', error);
        }
    };

    const handleEditDevice = (device) => {
        setEditingDevice(device);
    };

    const handleUpdateDevice = async () => {
        try {
            await updateIoTDevice(editingDevice.id, editingDevice);
            setEditingDevice(null);
            fetchIoTDevices();
        } catch (error) {
            console.error('Error updating IoT device:', error);
        }
    };

    const handleCancelEdit = () => {
        setEditingDevice(null);
    };

    const handleSort = (column) => {
        if (column === sortColumn) {
            setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
        } else {
            setSortColumn(column);
            setSortOrder('asc');
        }
    };

    const sortedDevices = useMemo(() => {
        if (!sortColumn) return iotDevices;

        return [...iotDevices].sort((a, b) => {
            if (a[sortColumn] < b[sortColumn]) return sortOrder === 'asc' ? -1 : 1;
            if (a[sortColumn] > b[sortColumn]) return sortOrder === 'asc' ? 1 : -1;
            return 0;
        });
    }, [iotDevices, sortColumn, sortOrder]);

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminIoTDeviceManagementPage.title"
                        defaultMessage="IoT Device Management"
                    />
                </h2>
                <table className="table">
                    <thead>
                    <tr>
                        <th onClick={() => handleSort('id')}>
                            ID {sortColumn === 'id' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('name')}>
                            <FormattedMessage
                                id="adminIoTDeviceManagementPage.name"
                                defaultMessage="Name"
                            />
                            {sortColumn === 'name' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('type')}>
                            <FormattedMessage
                                id="adminIoTDeviceManagementPage.type"
                                defaultMessage="Type"
                            />
                            {sortColumn === 'type' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th onClick={() => handleSort('isActive')}>
                            <FormattedMessage
                                id="adminIoTDeviceManagementPage.status"
                                defaultMessage="Status"
                            />
                            {sortColumn === 'isActive' && (sortOrder === 'asc' ? '▲' : '▼')}
                        </th>
                        <th>
                            <FormattedMessage
                                id="adminIoTDeviceManagementPage.edit"
                                defaultMessage="Edit"
                            />
                        </th>
                        <th>
                            <FormattedMessage
                                id="adminIoTDeviceManagementPage.delete"
                                defaultMessage="Delete"
                            />
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {sortedDevices.map((device) => (
                        <tr key={device.id}>
                            <td>{device.id}</td>
                            <td>
                                {editingDevice && editingDevice.id === device.id ? (
                                    <input
                                        type="text"
                                        value={editingDevice.name}
                                        onChange={(e) =>
                                            setEditingDevice({ ...editingDevice, name: e.target.value })
                                        }
                                    />
                                ) : (
                                    device.name
                                )}
                            </td>
                            <td>{getDeviceType(device.type, intl)}</td>
                            <td>
                                {editingDevice && editingDevice.id === device.id ? (
                                    <select
                                        value={editingDevice.isActive}
                                        onChange={(e) =>
                                            setEditingDevice({
                                                ...editingDevice,
                                                isActive: e.target.value === 'true',
                                            })
                                        }
                                    >
                                        <option value="true">
                                            {intl.formatMessage({ id: 'device.status.active' })}
                                        </option>
                                        <option value="false">
                                            {intl.formatMessage({ id: 'device.status.inactive' })}
                                        </option>
                                    </select>
                                ) : (
                                    getDeviceStatus(device.isActive, intl)
                                )}
                            </td>
                            <td>
                                {editingDevice && editingDevice.id === device.id ? (
                                    <>
                                        <button
                                            className="btn btn-primary btn-sm me-2"
                                            onClick={handleUpdateDevice}
                                        >
                                            <FormattedMessage
                                                id="adminIoTDeviceManagementPage.save"
                                                defaultMessage="Save"
                                            />
                                        </button>
                                        <button
                                            className="btn btn-secondary btn-sm"
                                            onClick={handleCancelEdit}
                                        >
                                            <FormattedMessage
                                                id="adminIoTDeviceManagementPage.cancel"
                                                defaultMessage="Cancel"
                                            />
                                        </button>
                                    </>
                                ) : (
                                    <button
                                        className="btn btn-primary btn-sm"
                                        onClick={() => handleEditDevice(device)}
                                    >
                                        <FormattedMessage
                                            id="adminIoTDeviceManagementPage.edit"
                                            defaultMessage="Edit"
                                        />
                                    </button>
                                )}
                            </td>
                            <td>
                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => handleDeleteDevice(device.id)}
                                >
                                    <FormattedMessage
                                        id="adminIoTDeviceManagementPage.delete"
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

export default AdminIoTDeviceManagementPage;