/**
 * @file consumption_repository.cpp
 * @brief Consumption repository implementation file for the IoT resource consumption monitoring system.
 */

#include "consumption_repository.h"

/**
 * @brief Get the unit from the server based on the resource type ID.
 * @param id The resource type ID.
 * @return The unit string.
 */
String ConsumptionRepository::getUnitFromServer(int id) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/ResourceType/" + String(id));
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
       return "";
     }

     return doc["unit"].as<String>();
   }

   http.end();
 }
 return "";
}

/**
* @brief Convert the consumption unit based on the tariff ID and initial unit.
* @param consumption The consumption value.
* @param tariffId The tariff ID.
* @param resourceType The resource type.
* @param initialUnit The initial unit.
* @return The converted consumption value.
*/
float ConsumptionRepository::convertConsumptionUnit(float consumption, int tariffId, ResourceType resourceType, const String& initialUnit) {
 if (WiFi.status() == WL_CONNECTED) {
   int resourceTypeId = TariffRepository::getResourceTypeIdFromTariff(tariffId);

   if (resourceTypeId != -1) {
     String unit = getUnitFromServer(resourceTypeId);

     if (unit != "") {
       float convertedConsumption = convertUnit(consumption, initialUnit, unit);
       return convertedConsumption;
     }
   }
 }

 return consumption;
}

/**
* @brief Convert the value from one unit to another.
* @param value The value to convert.
* @param fromUnit The source unit.
* @param toUnit The target unit.
* @return The converted value.
*/
float ConsumptionRepository::convertUnit(float value, String fromUnit, String toUnit) {
 if (fromUnit == toUnit) {
   return value;
 }

 if (fromUnit == "m³" && toUnit == "l") {
   return value * 1000;
 } else if (fromUnit == "m³" && toUnit == "gal") {
   return value * 264.172;
 } else if (fromUnit == "l" && toUnit == "m³") {
   return value / 1000;
 } else if (fromUnit == "l" && toUnit == "gal") {
   return value * 0.264172;
 } else if (fromUnit == "gal" && toUnit == "m³") {
   return value / 264.172;
 } else if (fromUnit == "gal" && toUnit == "l") {
   return value / 0.264172;
 }

 if (fromUnit == "m³" && toUnit == "ft³") {
   return value * 35.3147;
 } else if (fromUnit == "ft³" && toUnit == "m³") {
   return value / 35.3147;
 }

 if (fromUnit == "kWh" && toUnit == "MWh") {
   return value / 1000;
 } else if (fromUnit == "kWh" && toUnit == "J") {
   return value * 3.6e+6;
 } else if (fromUnit == "MWh" && toUnit == "kWh") {
   return value * 1000;
 } else if (fromUnit == "MWh" && toUnit == "J") {
   return value * 3.6e+9;
 } else if (fromUnit == "J" && toUnit == "kWh") {
   return value / 3.6e+6;
 } else if (fromUnit == "J" && toUnit == "MWh") {
   return value / 3.6e+9;
 }

 return value;
}