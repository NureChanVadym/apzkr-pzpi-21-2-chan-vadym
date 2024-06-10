/**
 * @file sensor_service.h
 * @brief Sensor service header file for the IoT resource consumption monitoring system.
 */

#ifndef SENSOR_SERVICE_H
#define SENSOR_SERVICE_H

#include <Arduino.h>
#include "config.h"

/**
 * @brief Read the LDR sensor value and convert it to lux.
 * @param pin The LDR sensor pin.
 * @return The lux value.
 */
float readLux(int pin);

#endif // SENSOR_SERVICE_H