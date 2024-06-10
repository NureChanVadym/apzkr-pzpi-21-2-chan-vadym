// pages/Services.js
import React from 'react';
import { FormattedMessage } from 'react-intl';
import '../styles/services.css';

const Services = () => {
    return (
        <main className="main py-5">
            <div className="container">
                <div className="row">
                    <div className="col">
                        <h1>
                            <FormattedMessage
                                id="services.title"
                                defaultMessage="Our Services"
                            />
                        </h1>
                        <ul>
                            <li>
                                <FormattedMessage
                                    id="services.service1"
                                    defaultMessage="Real-time monitoring of resource consumption"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service2"
                                    defaultMessage="Automated control and optimization of resource distribution"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service3"
                                    defaultMessage="Demand forecasting and resource usage planning"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service4"
                                    defaultMessage="Analytics and reporting on resource consumption"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service5"
                                    defaultMessage="Personalized recommendations for improving resource efficiency"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service6"
                                    defaultMessage="Integration with existing systems and infrastructure"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service7"
                                    defaultMessage="Alerts and notifications for critical situations"
                                />
                            </li>
                            <li>
                                <FormattedMessage
                                    id="services.service8"
                                    defaultMessage="User support and staff training"
                                />
                            </li>
                        </ul>
                        <p>
                            <FormattedMessage
                                id="services.description1"
                                defaultMessage="Our services are aimed at providing a comprehensive approach to resource management, covering all aspects from data collection to informed decision-making. We help our clients optimize water, electricity, and gas consumption, reduce costs, and minimize their carbon footprint."
                            />
                        </p>
                        <p>
                            <FormattedMessage
                                id="services.description2"
                                defaultMessage="With its flexible and scalable architecture, EcoMeChan can be adapted to the needs of various industries and organization sizes. We offer personalized solutions that take into account the specifics of your business and help you achieve maximum resource efficiency."
                            />
                        </p>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default Services;