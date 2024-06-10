/**
 * @file sensor_service.cpp
 * @brief Sensor service implementation file for the IoT resource consumption monitoring system.
 */

#include "sensor_service.h"

/**
 * @brief Read the LDR sensor value and convert it to lux.
 * @param pin The LDR sensor pin.
 * @return The lux value.
 */
float readLux(int pin) {
  int analogValue = analogRead(pin);
  float voltage = analogValue / 4095.0 * 5.0;
  float resistance = 2000 * voltage / (1 - voltage / 5.0);
  float lux = pow(ConfigSingleton::getInstance().rl10 * 1e3 * pow(10, ConfigSingleton::getInstance().gamma) / resistance, (1 / ConfigSingleton::getInstance().gamma));
  return lux;
}