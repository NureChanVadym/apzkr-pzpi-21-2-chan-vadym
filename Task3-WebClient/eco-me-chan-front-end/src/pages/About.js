import React from 'react';
import { FormattedMessage } from 'react-intl';
import '../styles/about.css';

const About = () => {
    return (
        <main className="main py-5">
            <div className="container">
                <div className="row">
                    <div className="col">
                        <h1>
                            <FormattedMessage
                                id="about.title"
                                defaultMessage="About Us"
                            />
                        </h1>
                        <p>
                            <FormattedMessage
                                id="about.description1"
                                defaultMessage="EcoMeChan is an innovative software system designed for efficient management and optimization of utility resource consumption, such as water, electricity, and gas. Our mission is to promote sustainable development and reduce the negative impact on the environment through smart resource allocation."
                            />
                        </p>
                        <p>
                            <FormattedMessage
                                id="about.description2"
                                defaultMessage="We strive to help cities, businesses, and individual users reduce utility costs, increase resource efficiency, and ensure reliable supply. Utilizing advanced technologies and intelligent data analysis, EcoMeChan provides powerful tools for real-time monitoring, control, and optimization of resource consumption."
                            />
                        </p>
                        <p>
                            <FormattedMessage
                                id="about.description3"
                                defaultMessage="Our team consists of experienced professionals in the field of software development, energy, and resource management. We are dedicated to creating environmentally responsible solutions that help our clients achieve their goals in sustainable development and resource efficiency."
                            />
                        </p>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default About;