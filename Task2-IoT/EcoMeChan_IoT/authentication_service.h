/**
 * @file authentication_service.h
 * @brief Authentication service header file for the IoT resource consumption monitoring system.
 */

#ifndef AUTHENTICATION_SERVICE_H
#define AUTHENTICATION_SERVICE_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "user_singleton.h"

/**
 * @brief Authenticate the user with the server.
 * @return True if authentication is successful, false otherwise.
 */
bool authenticateUser();

#endif // AUTHENTICATION_SERVICE_H