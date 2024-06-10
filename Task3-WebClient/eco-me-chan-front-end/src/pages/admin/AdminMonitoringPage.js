// src/pages/admin/AdminMonitoringPage.js
import React, { useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { format, parseISO } from 'date-fns';
import { getAllNotifications } from '../../services/NotificationService';
import { formatDate, formatTime } from '../../utils/DateUtils';

const AdminMonitoringPage = () => {
    const [notifications, setNotifications] = useState([]);
    const [sortColumn, setSortColumn] = useState(null);
    const [sortOrder, setSortOrder] = useState('asc');
    const intl = useIntl();
    const locale = intl.locale;

    useEffect(() => {
        fetchNotifications();
    }, []);

    const fetchNotifications = async () => {
        try {
            const notificationsData = await getAllNotifications();
            setNotifications(notificationsData);
        } catch (error) {
            console.error('Error fetching notifications:', error);
        }
    };

    const getNotificationType = (type) => {
        switch (type) {
            case 0:
                return intl.formatMessage({ id: 'notification.type.normal' });
            case 1:
                return intl.formatMessage({ id: 'notification.type.warning' });
            case 2:
                return intl.formatMessage({ id: 'notification.type.critical' });
            default:
                return intl.formatMessage({ id: 'notification.type.unknown' });
        }
    };

    const handleSort = (column) => {
        if (column === sortColumn) {
            setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
        } else {
            setSortColumn(column);
            setSortOrder('asc');
        }
    };

    const sortedNotifications = useMemo(() => {
        if (!sortColumn) return notifications;

        return [...notifications].sort((a, b) => {
            if (sortColumn === 'createdAt') {
                return sortOrder === 'asc'
                    ? new Date(a.createdAt) - new Date(b.createdAt)
                    : new Date(b.createdAt) - new Date(a.createdAt);
            } else {
                if (a[sortColumn] < b[sortColumn]) return sortOrder === 'asc' ? -1 : 1;
                if (a[sortColumn] > b[sortColumn]) return sortOrder === 'asc' ? 1 : -1;
                return 0;
            }
        });
    }, [notifications, sortColumn, sortOrder]);

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="adminMonitoringPage.title"
                        defaultMessage="IoT Notifications"
                    />
                </h2>
                <div className="row">
                    <div className="col">
                        <table className="table">
                            <thead>
                            <tr>
                                <th onClick={() => handleSort('id')}>
                                    ID {sortColumn === 'id' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                                <th onClick={() => handleSort('ioTDeviceId')}>
                                    <FormattedMessage
                                        id="adminMonitoringPage.iotDevice"
                                        defaultMessage="IoT Device"
                                    />
                                    {sortColumn === 'ioTDeviceId' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                                <th onClick={() => handleSort('notificationType')}>
                                    <FormattedMessage
                                        id="adminMonitoringPage.notificationType"
                                        defaultMessage="Type"
                                    />
                                    {sortColumn === 'notificationType' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                                <th onClick={() => handleSort('text')}>
                                    <FormattedMessage
                                        id="adminMonitoringPage.message"
                                        defaultMessage="Message"
                                    />
                                    {sortColumn === 'text' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                                <th onClick={() => handleSort('createdAt')}>
                                    <FormattedMessage
                                        id="adminMonitoringPage.date"
                                        defaultMessage="Date"
                                    />
                                    {sortColumn === 'createdAt' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                                <th onClick={() => handleSort('createdAt')}>
                                    <FormattedMessage
                                        id="adminMonitoringPage.time"
                                        defaultMessage="Time"
                                    />
                                    {sortColumn === 'createdAt' && (sortOrder === 'asc' ? '▲' : '▼')}
                                </th>
                            </tr>
                            </thead>
                            <tbody>
                            {sortedNotifications.map((notification) => (
                                <tr key={notification.id}>
                                    <td>{notification.id}</td>
                                    <td>{notification.ioTDeviceId}</td>
                                    <td>{getNotificationType(notification.notificationType)}</td>
                                    <td>{notification.text}</td>
                                    <td>{formatDate(notification.createdAt, locale)}</td>
                                    <td>{formatTime(notification.createdAt, locale)}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default AdminMonitoringPage;