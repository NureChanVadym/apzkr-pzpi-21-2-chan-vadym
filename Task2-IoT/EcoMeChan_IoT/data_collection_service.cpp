/**
 * @file data_collection_service.cpp
 * @brief Data collection service implementation file for the IoT resource consumption monitoring system.
 */

#include "data_collection_service.h"
#include "utils.h"


/**
 * @brief Send sensor data to the server.
 * @param value The sensor value.
 * @param deviceId The IoT device ID.
 */
void DataCollectionService::sendSensorDataToServer(float value, int deviceId) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/SensorData");
    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");
    DynamicJsonDocument doc(1024);
    doc["iotDeviceId"] = deviceId;
    doc["timestamp"] = getFormattedUtcTime();
    doc["value"] = value;

    String payload;
    serializeJson(doc, payload);

   Serial.println("Sending sensor data to server:");
   Serial.println(payload);

   int httpResponseCode = http.POST(payload);
   String response = http.getString();

   Serial.print("Sensor data sent to server. Response code: ");
   Serial.println(httpResponseCode);
   Serial.println("Response: " + response);

   http.end();
 }
}

/**
* @brief Send consumption data to the server.
* @param consumption The consumption value.
* @param tariffId The tariff ID.
*/
void DataCollectionService::sendConsumptionToServer(float consumption, int tariffId) {
 if (WiFi.status() == WL_CONNECTED) {
   HTTPClient http;
   http.begin(ConfigSingleton::getInstance().serverUrl + "api/Consumption");
   http.addHeader("Content-Type", "application/json");
   http.addHeader("ngrok-skip-browser-warning", "true");
   DynamicJsonDocument doc(1024);
   doc["userId"] = UserSingleton::getInstance().id;
   doc["tariffId"] = tariffId;
   doc["date"] = getFormattedUtcTime();
   doc["consumedAmount"] = consumption;

   String payload;
   serializeJson(doc, payload);

   Serial.println("Sending consumption data to server:");
   Serial.println(payload);

   int httpResponseCode = http.POST(payload);
   String response = http.getString();

   Serial.print("Consumption sent to server. Response code: ");
   Serial.println(httpResponseCode);
   Serial.println("Response: " + response);

   http.end();
 }
}