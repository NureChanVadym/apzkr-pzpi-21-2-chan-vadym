import React from 'react';
import { FormattedMessage } from 'react-intl';
import '../styles/home.css';

const Home = () => {
    return (
        <main className="main">
            <div className="hero">
                <div className="container">
                    <div className="row justify-content-center">
                        <div className="col-lg-8 text-center">
                            <h1>
                                <FormattedMessage
                                    id="home.welcome"
                                    defaultMessage="Welcome to Ecomechan"
                                />
                            </h1>
                            <p>
                                <FormattedMessage
                                    id="home.description"
                                    defaultMessage="Smart distribution of electricity, gas, and water resources."
                                />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default Home;