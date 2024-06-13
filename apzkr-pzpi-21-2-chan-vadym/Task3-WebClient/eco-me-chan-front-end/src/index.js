import React from 'react';
import ReactDOM from 'react-dom/client';
import { IntlProvider } from 'react-intl';
import App from './App';
import './styles/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { AuthProvider } from './contexts/AuthContext';
import { LanguageProvider } from './contexts/LanguageContext';
import en from './locales/en.json';
import uk from './locales/ua.json';
import '@formatjs/intl-numberformat/polyfill';
import '@formatjs/intl-numberformat/locale-data/en';
import '@formatjs/intl-numberformat/locale-data/uk';
import { fetchExchangeRates } from './services/CurrencyService';

const messages = {
    en,
    uk,
};

const root = ReactDOM.createRoot(document.getElementById('root'));

fetchExchangeRates()
    .then((exchangeRates) => {
        console.log("bebra");
        localStorage.setItem('exchangeRates', JSON.stringify(exchangeRates));
    })
    .catch((error) => {
        console.error('Failed to fetch exchange rates:', error);
    });


root.render(
    <React.StrictMode>
        <LanguageProvider>
            <AuthProvider>
                <App />
            </AuthProvider>
        </LanguageProvider>
    </React.StrictMode>
);