/**
 * @file data_collection_service.h
 * @brief Data collection service header file for the IoT resource consumption monitoring system.
 */

#ifndef DATA_COLLECTION_SERVICE_H
#define DATA_COLLECTION_SERVICE_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "user_singleton.h"

/**
 * @brief Class for handling data collection and sending to the server.
 */
class DataCollectionService {
public:
  /**
   * @brief Send sensor data to the server.
   * @param value The sensor value.
   * @param deviceId The IoT device ID.
   */
  void sendSensorDataToServer(float value, int deviceId);

  /**
   * @brief Send consumption data to the server.
   * @param consumption The consumption value.
   * @param tariffId The tariff ID.
   */
  void sendConsumptionToServer(float consumption, int tariffId);
};

#endif // DATA_COLLECTION_SERVICE_H