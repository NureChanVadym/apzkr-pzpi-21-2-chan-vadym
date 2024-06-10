/**
 * @file authentication_service.cpp
 * @brief Authentication service implementation file for the IoT resource consumption monitoring system.
 */

#include "authentication_service.h"

/**
 * @brief Authenticate the user with the server.
 * @return True if authentication is successful, false otherwise.
 */
bool authenticateUser() {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/User/authenticate");
    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");
    String payload = "{\"login\":\"" + ConfigSingleton::getInstance().userLogin + "\", \"password\":\"" + ConfigSingleton::getInstance().userPassword + "\"}";

    Serial.println("Sending authentication request to server...");
    Serial.println("POST " + ConfigSingleton::getInstance().serverUrl + "api/User/authenticate");
    Serial.println(payload);

    int httpResponseCode = http.POST(payload);

    Serial.println("HTTP Response code: ");
    Serial.println(httpResponseCode);

    if (httpResponseCode == 200) {
      String response = http.getString();
      Serial.println("Response:");
      Serial.println(response);

      DynamicJsonDocument doc(1024);
      DeserializationError error = deserializeJson(doc, response);

      if (error) {
        Serial.print(F("deserializeJson() failed: "));
        Serial.println(error.c_str());
        return false;
      }

      Serial.println("User authenticated successfully");

      UserSingleton::setUserData(doc.as<JsonObject>());

      Serial.println("User data stored in global user model object");

      return true;
    } else {
      Serial.println("User authentication failed");
      return false;
    }

    http.end();
  } else {
    Serial.println("Wi-Fi connection lost. Authentication not possible.");
  }
  return false;
}