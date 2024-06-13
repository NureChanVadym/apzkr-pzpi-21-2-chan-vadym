// src/pages/user/UserConsumptionHistoryPage.js
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { format, parseISO } from 'date-fns';
import { AuthContext } from '../../contexts/AuthContext';
import { getUserConsumptionHistory } from '../../services/ConsumptionService';
import ResourceType from '../../enums/ResourceType';
import { formatDate, formatTime } from '../../utils/DateUtils';


const UserConsumptionHistoryPage = () => {
    const { user } = useContext(AuthContext);
    const [consumptionHistory, setConsumptionHistory] = useState([]);
    const [sortColumn, setSortColumn] = useState(null);
    const [sortOrder, setSortOrder] = useState('asc');
    const [filters, setFilters] = useState({
        dateFrom: '',
        dateTo: '',
        amountFrom: '',
        amountTo: '',
        resourceType: '',
        costFrom: '',
        costTo: '',
    });
    const [errors, setErrors] = useState({
        dateFrom: '',
        dateTo: '',
        amountFrom: '',
        amountTo: '',
        costFrom: '',
        costTo: '',
    });
    const [openFilterMenu, setOpenFilterMenu] = useState(false);
    const intl = useIntl();
    const locale = intl.locale;

    useEffect(() => {
        const fetchUserConsumptionHistory = async () => {
            try {
                const history = await getUserConsumptionHistory(user.id);
                setConsumptionHistory(history);
            } catch (error) {
                console.error('Error fetching user consumption history:', error);
            }
        };

        if (user) {
            fetchUserConsumptionHistory();
        }
    }, [user]);

    const formatAmount = (amount) => {
        return Number(amount).toFixed(2);
    };

    const formatCost = (cost) => {
        return Number(cost).toFixed(2);
    };

    const getLocalizedResourceType = (resourceType) => {
        switch (resourceType) {
            case ResourceType.Water:
                return intl.formatMessage({ id: 'resourceType.water', defaultMessage: 'Water' });
            case ResourceType.Gas:
                return intl.formatMessage({ id: 'resourceType.gas', defaultMessage: 'Gas' });
            case ResourceType.Electricity:
                return intl.formatMessage({ id: 'resourceType.electricity', defaultMessage: 'Electricity' });
            default:
                return '';
        }
    };

    const convertUnit = (value, fromUnit, toUnit) => {
        if (fromUnit === toUnit) {
            return value;
        }

        switch (fromUnit) {
            // Water conversions
            case 'm³':
                if (toUnit === 'l') {
                    return value * 1000;
                } else if (toUnit === 'gal') {
                    return value * 264.172;
                } else if (toUnit === 'ft³') {
                    return value * 35.3147;
                }
                break;
            case 'l':
                if (toUnit === 'm³') {
                    return value / 1000;
                } else if (toUnit === 'gal') {
                    return value * 0.264172;
                }
                break;
            case 'gal':
                if (toUnit === 'm³') {
                    return value / 264.172;
                } else if (toUnit === 'l') {
                    return value / 0.264172;
                }
                break;

            // Gas conversions
            case 'ft³':
                if (toUnit === 'm³') {
                    return value / 35.3147;
                }
                break;

            // Electricity conversions
            case 'kWh':
                if (toUnit === 'MWh') {
                    return value / 1000;
                } else if (toUnit === 'J') {
                    return value * 3.6e6;
                }
                break;
            case 'MWh':
                if (toUnit === 'kWh') {
                    return value * 1000;
                } else if (toUnit === 'J') {
                    return value * 3.6e9;
                }
                break;
            case 'J':
                if (toUnit === 'kWh') {
                    return value / 3.6e6;
                } else if (toUnit === 'MWh') {
                    return value / 3.6e9;
                }
                break;
            default:
                return value;
        }
    };

    // Функція для отримання кінцевої одиниці вимірювання на основі типу ресурсу та локалі
    const getLocalizedUnit = (resourceType) => {
        switch (resourceType) {
            case ResourceType.Water:
                return locale === 'uk' ? 'm³' : 'gal';
            case ResourceType.Gas:
                return locale === 'uk' ? 'm³' : 'ft³';
            case ResourceType.Electricity:
                return 'kWh';
            default:
                return '';
        }
    };

    // Функція для перетворення значення з одиниці вимірювання, отриманої з сервера, у кінцеву одиницю вимірювання
    const convertAmount = (amount, resourceType, unitFrom) => {
        const localizedUnit = getLocalizedUnit(resourceType);
        return convertUnit(amount, unitFrom, localizedUnit);
    };

    const exchangeRates = JSON.parse(localStorage.getItem('exchangeRates'));


    const convertCurrency = (cost, fromCurrency, toCurrency) => {
        if (fromCurrency === toCurrency) {
            return cost;
        }

        const exchangeRate = exchangeRates[`${fromCurrency}_${toCurrency}`];
        return cost * exchangeRate;
    };

    const getLocalizedCurrency = (currencyCode) => {
        switch (currencyCode) {
            case 'USD':
                return intl.formatMessage({ id: 'currency.usd', defaultMessage: 'USD' });
            case 'EUR':
                return intl.formatMessage({ id: 'currency.eur', defaultMessage: 'EUR' });
            case 'UAH':
                return intl.formatMessage({ id: 'currency.uah', defaultMessage: 'UAH' });
            default:
                return currencyCode;
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

    const handleFilterChange = (e) => {
        const { name, value } = e.target;
        setFilters((prevFilters) => ({ ...prevFilters, [name]: value }));
    };

    const toggleFilterMenu = () => {
        setOpenFilterMenu(!openFilterMenu);
    };

    const validateFilters = () => {
        const newErrors = {
            dateFrom: '',
            dateTo: '',
            amountFrom: '',
            amountTo: '',
            costFrom: '',
            costTo: '',
        };

        if (filters.dateFrom && filters.dateTo) {
            const dateFrom = new Date(filters.dateFrom);
            const dateTo = new Date(filters.dateTo);
            dateTo.setDate(dateTo.getDate() + 1);

            if (dateFrom > dateTo) {
                newErrors.dateFrom = 'Date From cannot be greater than Date To';
                newErrors.dateTo = 'Date To cannot be less than Date From';
            }
        }

        if (filters.amountFrom && filters.amountTo && Number(filters.amountFrom) > Number(filters.amountTo)) {
            newErrors.amountFrom = 'Amount From cannot be greater than Amount To';
            newErrors.amountTo = 'Amount To cannot be less than Amount From';
        }

        if (filters.costFrom && filters.costTo && Number(filters.costFrom) > Number(filters.costTo)) {
            newErrors.costFrom = 'Cost From cannot be greater than Cost To';
            newErrors.costTo = 'Cost To cannot be less than Cost From';
        }

        setErrors(newErrors);

        return Object.values(newErrors).every((error) => error === '');
    };

    const filteredConsumptionHistory = useMemo(() => {
        if (!validateFilters()) {
            return [];
        }

        return consumptionHistory.filter((item) => {
            const date = new Date(item.date);
            const amount = Number(item.consumedAmount);
            const cost = Number(item.consumedCost);
            const dateFrom = filters.dateFrom ? new Date(filters.dateFrom) : null;
            const dateTo = filters.dateTo ? new Date(filters.dateTo) : null;

            if (dateFrom && date < dateFrom) {
                return false;
            }
            if (dateTo) {
                dateTo.setDate(dateTo.getDate() + 1);
                if (date > dateTo) {
                    return false;
                }
            }
            if (filters.amountFrom && amount < Number(filters.amountFrom)) {
                return false;
            }
            if (filters.amountTo && amount > Number(filters.amountTo)) {
                return false;
            }
            if (filters.resourceType && item.resourceType !== Number(filters.resourceType)) {
                return false;
            }
            if (filters.costFrom && cost < Number(filters.costFrom)) {
                return false;
            }
            if (filters.costTo && cost > Number(filters.costTo)) {
                return false;
            }

            return true;
        });
    }, [consumptionHistory, filters]);

    const sortedConsumptionHistory = useMemo(() => {
        if (!sortColumn) return filteredConsumptionHistory;

        return [...filteredConsumptionHistory].sort((a, b) => {
            if (a[sortColumn] < b[sortColumn]) return sortOrder === 'asc' ? -1 : 1;
            if (a[sortColumn] > b[sortColumn]) return sortOrder === 'asc' ? 1 : -1;
            return 0;
        });
    }, [filteredConsumptionHistory, sortColumn, sortOrder]);

    return (
        <main className="main py-5">
            <div className="container">
                <h2>
                    <FormattedMessage
                        id="userConsumptionHistoryPage.title"
                        defaultMessage="Consumption History"
                    />
                </h2>

                {/* Випадаюче меню фільтрів */}
                <div className="dropdown mb-3">
                    <button
                        className="btn btn-secondary dropdown-toggle"
                        type="button"
                        onClick={toggleFilterMenu}
                    >
                        <FormattedMessage
                            id="userConsumptionHistoryPage.filterMenu"
                            defaultMessage="Filters"
                        />
                    </button>
                    {openFilterMenu && (
                        <div className="dropdown-menu show">
                            <div className="px-3 py-2">
                                {/* Фільтр за датою */}
                                <div className="row mb-3">
                                    <div className="col-md-6">
                                        <label htmlFor="dateFrom">
                                            <FormattedMessage
                                                id="userConsumptionHistoryPage.dateFrom"
                                                defaultMessage="Date From"
                                            />
                                        </label>
                                        <input
                                            type="text"
                                            id="dateFrom"
                                            name="dateFrom"
                                            className="form-control"
                                            value={filters.dateFrom}
                                            onChange={handleFilterChange}
                                            placeholder={intl.formatMessage({
                                                id: 'userConsumptionHistoryPage.dateFromPlaceholder',
                                            })}
                                            onFocus={(e) => (e.target.type = 'date')}
                                            onBlur={(e) => (e.target.type = 'text')}
                                        />
                                    </div>
                                    <div className="col-md-6">
                                        <label htmlFor="dateTo">
                                            <FormattedMessage
                                                id="userConsumptionHistoryPage.dateTo"
                                                defaultMessage="Date To"
                                            />
                                        </label>
                                        <input
                                            type="text"
                                            id="dateTo"
                                            name="dateTo"
                                            className="form-control"
                                            value={filters.dateTo}
                                            onChange={handleFilterChange}
                                            placeholder={intl.formatMessage({
                                                id: 'userConsumptionHistoryPage.dateToPlaceholder',
                                            })}
                                            onFocus={(e) => (e.target.type = 'date')}
                                            onBlur={(e) => (e.target.type = 'text')}
                                        />
                                    </div>
                                </div>

                                {/* Фільтр за кількістю */}
                                <div className="mb-3">
                                    <label htmlFor="amountFrom">
                                        <FormattedMessage
                                            id="userConsumptionHistoryPage.amountFrom"
                                            defaultMessage="Amount From"
                                        />
                                    </label>
                                    <input
                                        type="number"
                                        id="amountFrom"
                                        name="amountFrom"
                                        className={`form-control ${errors.amountFrom ? 'is-invalid' : ''}`}
                                        value={filters.amountFrom}
                                        onChange={handleFilterChange}
                                    />
                                    {errors.amountFrom && (
                                        <div className="invalid-feedback">{errors.amountFrom}</div>
                                    )}
                                </div>
                                <div className="mb-3">
                                    <label htmlFor="amountTo">
                                        <FormattedMessage
                                            id="userConsumptionHistoryPage.amountTo"
                                            defaultMessage="Amount To"
                                        />
                                    </label>
                                    <input
                                        type="number"
                                        id="amountTo"
                                        name="amountTo"
                                        className={`form-control ${errors.amountTo ? 'is-invalid' : ''}`}
                                        value={filters.amountTo}
                                        onChange={handleFilterChange}
                                    />
                                    {errors.amountTo && (
                                        <div className="invalid-feedback">{errors.amountTo}</div>
                                    )}
                                </div>

                                {/* Фільтр за типом ресурсу */}
                                <div className="mb-3">
                                    <label htmlFor="resourceType">
                                        <FormattedMessage
                                            id="userConsumptionHistoryPage.resourceType"
                                            defaultMessage="Resource Type"
                                        />
                                    </label>
                                    <select
                                        id="resourceType"
                                        name="resourceType"
                                        className="form-control"
                                        value={filters.resourceType}
                                        onChange={handleFilterChange}
                                    >
                                        <option value="">
                                            <FormattedMessage
                                                id="userConsumptionHistoryPage.allResourceTypes"
                                                defaultMessage="All"
                                            />
                                        </option>
                                        <option value={ResourceType.Water}>
                                            {getLocalizedResourceType(ResourceType.Water)}
                                        </option>
                                        <option value={ResourceType.Gas}>
                                            {getLocalizedResourceType(ResourceType.Gas)}
                                        </option>
                                        <option value={ResourceType.Electricity}>
                                            {getLocalizedResourceType(ResourceType.Electricity)}
                                        </option>
                                    </select>
                                </div>

                                {/* Фільтр за вартістю */}
                                <div className="mb-3">
                                    <label htmlFor="costFrom">
                                        <FormattedMessage
                                            id="userConsumptionHistoryPage.costFrom"
                                            defaultMessage="Cost From"
                                        />
                                    </label>
                                    <input
                                        type="number"
                                        id="costFrom"
                                        name="costFrom"
                                        className={`form-control ${errors.costFrom ? 'is-invalid' : ''}`}
                                        value={filters.costFrom}
                                        onChange={handleFilterChange}
                                    />
                                    {errors.costFrom && (
                                        <div className="invalid-feedback">{errors.costFrom}</div>
                                    )}
                                </div>
                                <div>
                                    <label htmlFor="costTo">
                                        <FormattedMessage
                                            id="userConsumptionHistoryPage.costTo"
                                            defaultMessage="Cost To"
                                        />
                                    </label>
                                    <input
                                        type="number"
                                        id="costTo"
                                        name="costTo"
                                        className={`form-control ${errors.costTo ? 'is-invalid' : ''}`}
                                        value={filters.costTo}
                                        onChange={handleFilterChange}
                                    />
                                    {errors.costTo && (
                                        <div className="invalid-feedback">{errors.costTo}</div>
                                    )}
                                </div>
                            </div>
                        </div>
                    )}
                </div>

                <table className="table table-sort">
                    <thead>
                    <tr>
                        <th onClick={() => handleSort('date')}>
                            <FormattedMessage
                                id="userConsumptionHistoryPage.date"
                                defaultMessage="Date"
                            />
                            {sortColumn === 'date' && (
                                <i className={`fas ${sortOrder === 'asc' ? 'fa-sort-up' : 'fa-sort-down'}`}></i>
                            )}
                        </th>
                        <th onClick={() => handleSort('date')}>
                            <FormattedMessage
                                id="userConsumptionHistoryPage.time"
                                defaultMessage="Time"
                            />
                            {sortColumn === 'date' && (
                                <i className={`fas ${sortOrder === 'asc' ? 'fa-sort-up' : 'fa-sort-down'}`}></i>
                            )}
                        </th>
                        <th onClick={() => handleSort('resourceType')}>
                            <FormattedMessage
                                id="userConsumptionHistoryPage.resourceType"
                                defaultMessage="Resource Type"
                            />
                            {sortColumn === 'resourceType' && (
                                <i className={`fas ${sortOrder === 'asc' ? 'fa-sort-up' : 'fa-sort-down'}`}></i>
                            )}
                        </th>
                        <th onClick={() => handleSort('consumedAmount')}>
                            <FormattedMessage
                                id="userConsumptionHistoryPage.amount"
                                defaultMessage="Amount"
                            />
                            {sortColumn === 'consumedAmount' && (
                                <i className={`fas ${sortOrder === 'asc' ? 'fa-sort-up' : 'fa-sort-down'}`}></i>
                            )}
                        </th>
                        <th onClick={() => handleSort('consumedCost')}>
                            <FormattedMessage
                                id="userConsumptionHistoryPage.cost"
                                defaultMessage="Cost"
                            />
                            {sortColumn === 'consumedCost' && (
                                <i className={`fas ${sortOrder === 'asc' ? 'fa-sort-up' : 'fa-sort-down'}`}></i>
                            )}
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {sortedConsumptionHistory.map((item) => {
                        const localizedCurrency = locale === 'uk' ? 'UAH' : 'USD';
                        const convertedCost = convertCurrency(item.consumedCost, item.currencyCode, localizedCurrency);

                        const localizedUnit = getLocalizedUnit(item.resourceType);
                        const convertedAmount = convertAmount(
                            item.consumedAmount,
                            item.resourceType,
                            item.unit
                        );

                        return (
                            <tr key={item.id}>
                                <td>{formatDate(item.date, locale)}</td>
                                <td>{formatTime(item.date, locale)}</td>
                                <td>{getLocalizedResourceType(item.resourceType)}</td>
                                <td>
                                    {formatAmount(convertedAmount)} {localizedUnit}
                                </td>
                                <td>
                                    {formatCost(convertedCost)} {getLocalizedCurrency(localizedCurrency)}
                                </td>
                            </tr>
                        );
                    })}
                    </tbody>
                </table>
            </div>
        </main>
    );
};

export default UserConsumptionHistoryPage;