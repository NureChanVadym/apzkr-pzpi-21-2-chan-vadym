/**
 * @file tariff_repository.cpp
 * @brief Tariff repository implementation file for the IoT resource consumption monitoring system.
 */

#include "tariff_repository.h"

/**
 * @brief Get the resource type ID from the tariff ID.
 * @param tariffId The tariff ID.
 * @return The resource type ID.
 */
int TariffRepository::getResourceTypeIdFromTariff(int tariffId) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/Tariff/" + String(tariffId));
    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");

    int httpResponseCode = http.GET();

    if (httpResponseCode == 200) {
      String response = http.getString();
      DynamicJsonDocument doc(1024);
      DeserializationError error = deserializeJson(doc, response);

      if (error) {
        Serial.print(F("deserializeJson() failed: "));
        Serial.println(error.c_str());
        return -1;
      }

      return doc["resourceTypeId"].as<int>();
    }

    http.end();
  }
  return -1;
}