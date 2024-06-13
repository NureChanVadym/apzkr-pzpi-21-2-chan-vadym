/**
 * @file tariff_repository.h
 * @brief Tariff repository header file for the IoT resource consumption monitoring system.
 */

#ifndef TARIFF_REPOSITORY_H
#define TARIFF_REPOSITORY_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "config.h"

/**
 * @brief Repository class for accessing tariff data from the server.
 */
class TariffRepository {
public:
  /**
   * @brief Get the resource type ID from the tariff ID.
   * @param tariffId The tariff ID.
   * @return The resource type ID.
   */
  static int getResourceTypeIdFromTariff(int tariffId);
};

#endif // TARIFF_REPOSITORY_H