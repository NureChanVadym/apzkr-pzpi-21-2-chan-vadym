// src/components/Footer.js
import React from 'react';
import { Link } from 'react-router-dom';
import { FormattedMessage } from 'react-intl';
import '../styles/footer.css';

const Footer = ({ title, links, contactInfo }) => {
    return (
        <footer className="footer">
            <div className="container">
                <div className="row">
                    <div className="col-md-4 mb-4 mb-md-0">
                        <h3>{title}</h3>
                        <p>
                            <FormattedMessage
                                id="app.description"
                                defaultMessage="Smart distribution of electricity, gas, and water resources."
                            />
                        </p>
                    </div>
                    <div className="col-md-4 mb-4 mb-md-0">
                        <h3>
                            <FormattedMessage id="footer.links" defaultMessage="Links" />
                        </h3>
                        <ul className="list-unstyled">
                            {links.map((link) => (
                                <li key={link.id}>
                                    <Link to={link.path}>
                                        <FormattedMessage id={link.labelId} defaultMessage={link.defaultLabel} />
                                    </Link>
                                </li>
                            ))}
                        </ul>
                    </div>
                    <div className="col-md-4">
                        <h3>
                            <FormattedMessage id="footer.contacts" defaultMessage="Contacts" />
                        </h3>
                        {contactInfo.map((info) => (
                            <p key={info.id}>
                                <FormattedMessage
                                    id={info.labelId}
                                    defaultMessage={info.defaultLabel}
                                    values={info.values}
                                />
                            </p>
                        ))}
                    </div>
                </div>
                <div className="row mt-4">
                    <div className="col text-center">
                        <p className="text-muted">
                            <FormattedMessage
                                id="footer.copyright"
                                defaultMessage="{copyright} 2023 Ecomechan. All rights reserved."
                                values={{copyright: <span>©</span>}}
                            />
                        </p>
                    </div>
                </div>
            </div>
        </footer>
    );
};

export default Footer;