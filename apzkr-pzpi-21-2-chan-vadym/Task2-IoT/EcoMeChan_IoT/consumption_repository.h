/**
 * @file consumption_repository.h
 * @brief Consumption repository header file for the IoT resource consumption monitoring system.
 */

#ifndef CONSUMPTION_REPOSITORY_H
#define CONSUMPTION_REPOSITORY_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "tariff_repository.h"
#include "resource_type.h"

/**
 * @brief Repository class for accessing consumption data from the server.
 */
class ConsumptionRepository {
public:
  /**
   * @brief Get the unit from the server based on the resource type ID.
   * @param id The resource type ID.
   * @return The unit string.
   */
  String getUnitFromServer(int id);

  /**
   * @brief Convert the consumption unit based on the tariff ID and initial unit.
   * @param consumption The consumption value.
   * @param tariffId The tariff ID.
   * @param resourceType The resource type.
   * @param initialUnit The initial unit.
   * @return The converted consumption value.
   */
  float convertConsumptionUnit(float consumption, int tariffId, ResourceType resourceType, const String& initialUnit);

  /**
   * @brief Convert the value from one unit to another.
   * @param value The value to convert.
   * @param fromUnit The source unit.
   * @param toUnit The target unit.
   * @return The converted value.
   */
  float convertUnit(float value, String fromUnit, String toUnit);
};

#endif // CONSUMPTION_REPOSITORY_H