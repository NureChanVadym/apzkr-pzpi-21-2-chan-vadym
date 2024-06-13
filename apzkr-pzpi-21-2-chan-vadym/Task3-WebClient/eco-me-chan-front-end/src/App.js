import React, { useContext, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './pages/Home';
import About from './pages/About';
import Services from './pages/Services';
import Contacts from './pages/Contacts';
import PrivacyPolicy from './pages/PrivacyPolicy';
import TermsOfUse from './pages/TermsOfUse';
import Registration from './pages/Registration';
import Login from './pages/Login';
import UserHomePage from './pages/user/UserHomePage';
import AdminHomePage from './pages/admin/AdminHomePage';
import UserAccountPage from './pages/user/UserAccountPage';
import AdminAccountPage from './pages/admin/AdminAccountPage';
import UserConsumptionHistoryPage from './pages/user/UserConsumptionHistoryPage';
import UserIoTDevicesPage from './pages/user/UserIoTDevicesPage';
import AdminAccountManagementPage from './pages/admin/AdminAccountManagementPage';
import AdminCreateUserPage from './pages/admin/AdminCreateUserPage';
import AdminIoTDeviceManagementPage from './pages/admin/AdminIoTDeviceManagementPage';
import AdminMonitoringPage from './pages/admin/AdminMonitoringPage';
import AdminBackupManagementPage from './pages/admin/AdminBackupManagementPage';
import { FormattedMessage } from 'react-intl';
import { AuthContext } from './contexts/AuthContext';
import Role from './enums/Role';
import { IntlProvider } from 'react-intl';
import { LanguageContext } from './contexts/LanguageContext';
import en from './locales/en.json';
import uk from './locales/ua.json';
import 'bootstrap/dist/css/bootstrap.min.css';

const messages = {
    en,
    uk,
};

function App() {
    const { user, setUser } = useContext(AuthContext);
    const { language } = useContext(LanguageContext);


    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            setUser(JSON.parse(storedUser));
        }
    }, []);

    const renderHeader = () => {
        if (!user) {
            return (
                <Header
                    title={<FormattedMessage id="app.title" defaultMessage="Ecomechan" />}
                    navigationItems={[
                        { id: 'home', path: '/', labelId: 'app.home', defaultLabel: 'Home' },
                        { id: 'about', path: '/about', labelId: 'app.about', defaultLabel: 'About' },
                        { id: 'services', path: '/services', labelId: 'app.services', defaultLabel: 'Services' },
                        { id: 'contacts', path: '/contacts', labelId: 'app.contacts', defaultLabel: 'Contacts' },
                    ]}
                    authButtons={[
                        { id: 'login', path: '/login', labelId: 'app.login', defaultLabel: 'Login' },
                        { id: 'register', path: '/registration', labelId: 'app.register', defaultLabel: 'Register' },
                    ]}
                />
            );
        }

        switch (user?.role) {
            case Role.User:
                return (
                    <Header
                        title={<FormattedMessage id="userHeader.title" defaultMessage="User Dashboard" />}
                        navigationItems={[
                            { id: 'home', path: '/user', labelId: 'userHeader.home', defaultLabel: 'Home' },
                            { id: 'consumptionHistory', path: '/user/consumption-history', labelId: 'userHeader.consumptionHistory', defaultLabel: 'Consumption History' },
                            { id: 'iotDevices', path: '/user/iot-devices', labelId: 'userHeader.iotDevices', defaultLabel: 'IoT Devices' },
                        ]}
                        accountButton={{ path: '/user/account', labelId: 'userHeader.account', defaultLabel: 'Account' }}
                    />
                );
            case Role.Admin:
                return (
                    <Header
                        title={<FormattedMessage id="adminHeader.title" defaultMessage="Admin Dashboard" />}
                        navigationItems={[
                            { id: 'home', path: '/admin', labelId: 'adminHeader.home', defaultLabel: 'Home' },
                            { id: 'accountManagement', path: '/admin/account-management', labelId: 'adminHeader.accountManagement', defaultLabel: 'Account Management' },
                            { id: 'iotDeviceManagement', path: '/admin/iot-device-management', labelId: 'adminHeader.iotDeviceManagement', defaultLabel: 'IoT Device Management' },
                            { id: 'monitoring', path: '/admin/monitoring', labelId: 'adminHeader.monitoring', defaultLabel: 'Monitoring' },
                            { id: 'backupManagement', path: '/admin/backup-management', labelId: 'adminHeader.backupManagement', defaultLabel: 'Backup Management' },
                        ]}
                        accountButton={{ path: '/admin/account', labelId: 'adminHeader.account', defaultLabel: 'Account' }}
                    />
                );
            case Role.MunicipalResourceManager:
            default:
                return (
                    <Header
                        title={<FormattedMessage id="app.title" defaultMessage="Ecomechan" />}
                        navigationItems={[]}
                        authButtons={[]}
                    />
                );
        }
    };

    const renderFooter = () => {
        if (!user) {
            return (
                <Footer
                    title={<FormattedMessage id="app.title" defaultMessage="Ecomechan" />}
                    links={[
                        { id: 'about', path: '/about', labelId: 'footer.about', defaultLabel: 'About Us' },
                        { id: 'contacts', path: '/contacts', labelId: 'footer.contacts', defaultLabel: 'Contacts' },
                        { id: 'privacyPolicy', path: '/privacy-policy', labelId: 'footer.privacyPolicy', defaultLabel: 'Privacy Policy' },
                        { id: 'termsOfUse', path: '/terms-of-use', labelId: 'footer.termsOfUse', defaultLabel: 'Terms of Use' },
                    ]}
                    contactInfo={[
                        { id: 'address', labelId: 'footer.address', defaultLabel: 'Address: 14 Nauki Ave, Kharkiv, Kharkiv Oblast, 61166' },
                        { id: 'phone', labelId: 'footer.phone', defaultLabel: 'Phone: {phoneNumber}', values: { phoneNumber: <a href="tel:+380577021013">+38 (057) 702-10-13</a> } },
                        { id: 'email', labelId: 'footer.email', defaultLabel: 'Email: {email}', values: { email: <a href="mailto:info@nure.ua">info@nure.ua</a> } },
                    ]}
                />
            );
        }

        switch (user?.role) {
            case Role.User:
                return (
                    <Footer
                        title={<FormattedMessage id="userFooter.title" defaultMessage="User Dashboard" />}
                        links={[
                            { id: 'home', path: '/user', labelId: 'userHeader.home', defaultLabel: 'Home' },
                            { id: 'consumptionHistory', path: '/user/consumption-history', labelId: 'userHeader.consumptionHistory', defaultLabel: 'Consumption History' },
                            { id: 'iotDevices', path: '/user/iot-devices', labelId: 'userHeader.iotDevices', defaultLabel: 'IoT Devices' },
                        ]}
                        contactInfo={[
                            { id: 'address', labelId: 'footer.address', defaultLabel: 'Address: 14 Nauki Ave, Kharkiv, Kharkiv Oblast, 61166' },
                            { id: 'phone', labelId: 'footer.phone', defaultLabel: 'Phone: {phoneNumber}', values: { phoneNumber: <a href="tel:+380577021013">+38 (057) 702-10-13</a> } },
                            { id: 'email', labelId: 'footer.email', defaultLabel: 'Email: {email}', values: { email: <a href="mailto:info@nure.ua">info@nure.ua</a> } },
                        ]}
                    />
                );
            case Role.Admin:
                return (
                    <Footer
                        title={<FormattedMessage id="adminFooter.title" defaultMessage="Admin Dashboard" />}
                        links={[
                            { id: 'home', path: '/admin', labelId: 'adminHeader.home', defaultLabel: 'Home' },
                            { id: 'accountManagement', path: '/admin/account-management', labelId: 'adminHeader.accountManagement', defaultLabel: 'Account Management' },
                            { id: 'iotDeviceManagement', path: '/admin/iot-device-management', labelId: 'adminHeader.iotDeviceManagement', defaultLabel: 'IoT Device Management' },
                            { id: 'monitoring', path: '/admin/monitoring', labelId: 'adminHeader.monitoring', defaultLabel: 'Monitoring' },
                            { id: 'monitoring', path: '/admin/backup-management', labelId: 'adminHeader.backupManagement', defaultLabel: 'Backup Management' },
                        ]}
                        contactInfo={[
                            { id: 'address', labelId: 'footer.address', defaultLabel: 'Address: 14 Nauki Ave, Kharkiv, Kharkiv Oblast, 61166' },
                            { id: 'phone', labelId: 'footer.phone', defaultLabel: 'Phone: {phoneNumber}', values: { phoneNumber: <a href="tel:+380577021013">+38 (057) 702-10-13</a> } },
                            { id: 'email', labelId: 'footer.email', defaultLabel: 'Email: {email}', values: { email: <a href="mailto:info@nure.ua">info@nure.ua</a> } },
                        ]}
                    />
                );
            case Role.MunicipalResourceManager:
            default:
                return (
                    <Footer
                        title={<FormattedMessage id="app.title" defaultMessage="Ecomechan" />}
                        links={[]}
                        contactInfo={[]}
                    />
                );
        }
    };

    return (
        <AuthProvider>
            <IntlProvider messages={messages[language]} locale={language} defaultLocale="en">
                <Router>
                    <div className="app">
                        {renderHeader()}
                        <Routes>
                            <Route exact path="/" element={<Home />} />
                            <Route path="/about" element={<About />} />
                            <Route path="/services" element={<Services />} />
                            <Route path="/contacts" element={<Contacts />} />
                            <Route path="/privacy-policy" element={<PrivacyPolicy />} />
                            <Route path="/terms-of-use" element={<TermsOfUse />} />
                            <Route path="/login" element={<Login />} />
                            <Route path="/registration" element={<Registration />} />
                            <Route path="/user" element={user && user.role === Role.User ? <UserHomePage /> : <Navigate to="/" />} />
                            <Route path="/admin" element={user && user.role === Role.Admin ? <AdminHomePage /> : <Navigate to="/" />} />
                            <Route path="/user/account" element={<UserAccountPage />} />
                            <Route path="/admin/account" element={<AdminAccountPage />} />
                            <Route path="/user/consumption-history" element={<UserConsumptionHistoryPage />} />
                            <Route path="/user/iot-devices" element={<UserIoTDevicesPage />} />
                            <Route path="/admin/account-management" element={<AdminAccountManagementPage />} />
                            <Route path="/admin/create-user" element={<AdminCreateUserPage />} />
                            <Route path="/admin/iot-device-management" element={<AdminIoTDeviceManagementPage />} />
                            <Route path="/admin/monitoring" element={<AdminMonitoringPage />} />
                            <Route path="/admin/backup-management" element={<AdminBackupManagementPage />} />
                        </Routes>
                        {renderFooter()}
                    </div>
                </Router>
            </IntlProvider>
        </AuthProvider>
    );
}

export default App;