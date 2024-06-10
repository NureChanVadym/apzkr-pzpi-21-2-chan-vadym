// src/utils/DeviceUtils.js
import { FormattedMessage } from 'react-intl';

export const getDeviceStatus = (isActive, intl) => {
    if (isActive === true) {
        return intl.formatMessage({ id: 'device.status.active' });
    } else if (isActive === false) {
        return intl.formatMessage({ id: 'device.status.inactive' });
    } else {
        return intl.formatMessage({ id: 'device.status.unknown' });
    }
};

export const getDeviceType = (type, intl) => {
    switch (type) {
        case 0:
            return intl.formatMessage({ id: 'device.type.waterSensor' });
        case 1:
            return intl.formatMessage({ id: 'device.type.gasSensor' });
        case 2:
            return intl.formatMessage({ id: 'device.type.electricitySensor' });
        default:
            return intl.formatMessage({ id: 'device.type.unknown' });
    }
};