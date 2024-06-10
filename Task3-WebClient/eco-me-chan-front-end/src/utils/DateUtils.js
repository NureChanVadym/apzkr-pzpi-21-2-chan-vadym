// src/utils/DateUtils.js
import { format, parseISO } from 'date-fns';

export const formatDate = (dateString, locale) => {
    const date = parseISO(dateString);
    const formatPattern = locale === 'uk' ? 'dd/MM/yyyy' : 'MM/dd/yyyy';
    return format(date, formatPattern);
};

export const formatTime = (dateString, locale) => {
    const date = parseISO(dateString);
    const formatPattern = locale === 'uk' ? 'HH:mm:ss' : 'h:mm:ss a';
    return format(date, formatPattern);
};