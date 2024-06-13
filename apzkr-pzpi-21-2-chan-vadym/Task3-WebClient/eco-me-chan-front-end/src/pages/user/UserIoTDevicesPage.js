// src/pages/user/UserIoTDevicesPage.js
import React, { useContext, useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { AuthContext } from '../../contexts/AuthContext';
import { getUserIoTDevices } from '../../services/IoTDeviceService';
import { getDeviceStatus, getDeviceType } from '../../utils/DeviceUtils';

const UserIoTDevicesPage = () => {
    const { user } = useContext(AuthContext);
    const [iotDevices, setIoTDevices] = useState([]);
    const intl = useIntl();

    useEffect(() => {
        const fetchUserIoTDevices = async () => {
            try {
                const devices = await getUserIoTDevices(user.id);
                setIoTDevices(devices);
            } catch (error) {
                console.error('Error fetching user IoT devices:', error);
            }
        };

        if (user) {
            fetchUserIoTDevices();
        }
    }, [user]);

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="userIoTDevicesPage.title"
                        defaultMessage="IoT Devices"
                    />
                </h2>
                <div className="row">
                    {iotDevices.map(device => (
                        <div className="col-md-4 mb-4" key={device.id}>
                            <div className="card">
                                <div className="card-body">
                                    <h5 className="card-title">{device.name}</h5>
                                    <p className="card-text">
                                        <FormattedMessage
                                            id="userIoTDevicesPage.deviceType"
                                            defaultMessage="Type: {deviceType}"
                                            values={{ deviceType: getDeviceType(device.type, intl) }}
                                        />
                                    </p>
                                    <p className="card-text">
                                        <FormattedMessage
                                            id="userIoTDevicesPage.deviceStatus"
                                            defaultMessage="Status: {deviceStatus}"
                                            values={{ deviceStatus: getDeviceStatus(device.isActive, intl) }}
                                        />
                                    </p>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </main>
    );
};

export default UserIoTDevicesPage;