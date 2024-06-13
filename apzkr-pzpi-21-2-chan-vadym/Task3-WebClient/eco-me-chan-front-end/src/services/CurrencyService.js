import axios from 'axios';
import config from '../config.json';

export const fetchExchangeRates = async () => {
    try {
        const response = await axios.get(`${config.EXCHANGE_BASE_URL}/${config.EXCHANGE_API_KEY}/latest/UAH`);
        const rates = response.data.conversion_rates;

        const exchangeRates = {
            UAH_USD: rates.USD,
            UAH_EUR: rates.EUR,
            USD_EUR: rates.EUR / rates.USD,
            USD_UAH: 1 / rates.USD,
            EUR_UAH: 1 / rates.EUR,
            EUR_USD: rates.USD / rates.EUR,
            UAH_UAH: 1,
            USD_USD: 1,
            EUR_EUR: 1,
        };

        return exchangeRates;
    } catch (error) {
        console.error('Error fetching exchange rates:', error);
        throw error;
    }
};