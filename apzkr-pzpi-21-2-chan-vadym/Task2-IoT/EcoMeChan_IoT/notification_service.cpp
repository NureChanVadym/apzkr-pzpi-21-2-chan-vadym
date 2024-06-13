/**
 * @file notification_service.cpp
 * @brief Notification service implementation file for the IoT resource consumption monitoring system.
 */

#include "notification_service.h"
#include "utils.h"

/**
 * @brief Send a notification to the server.
 * @param userId The user ID.
 * @param iotDeviceId The IoT device ID.
 * @param notificationType The notification type.
 * @param text The notification text.
 */
void NotificationService::sendNotificationToServer(int userId, int iotDeviceId, NotificationType notificationType, const String& text) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/Notification");
    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");
    DynamicJsonDocument doc(1024);
    doc["userId"] = userId;
    doc["iotDeviceId"] = iotDeviceId;
    doc["notificationType"] = static_cast<int>(notificationType);
    doc["createdAt"] = getFormattedUtcTime();
    doc["text"] = text;

    String payload;
    serializeJson(doc, payload);

    Serial.println("Sending notification to server:");
    Serial.println(payload);

    int httpResponseCode = http.POST(payload);
    String response = http.getString();

    Serial.print("Notification sent to server. Response code: ");
    Serial.println(httpResponseCode);
    Serial.println("Response: " + response);

    http.end();
  }
}

/**
 * @brief Send consumption statistics to the server.
 * @param statsText The statistics text.
 * @param deviceId The IoT device ID.
 */
void NotificationService::sendStatsToServer(const String& statsText, int deviceId) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(ConfigSingleton::getInstance().serverUrl + "api/Notification");
    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");
    DynamicJsonDocument doc(1024);
    doc["userId"] = UserSingleton::getInstance().id;
    doc["iotDeviceId"] = deviceId;
    doc["notificationType"] = static_cast<int>(NotificationType::Normal);
    doc["createdAt"] = getFormattedUtcTime();
    doc["text"] = statsText;

    String payload;
    serializeJson(doc, payload);

    Serial.println("Sending stats to server:");
    Serial.println(payload);

    int httpResponseCode = http.POST(payload);
    String response = http.getString();

    Serial.print("Stats sent to server. Response code: ");
    Serial.println(httpResponseCode);
    Serial.println("Response: " + response);

    http.end();
  }
}

/**
 * @brief Check sensor values and send notifications based on thresholds.
 * @param waterValue The water sensor value.
 * @param gasValue The gas sensor value.
 * @param electricityValue The electricity sensor value.
 */
void NotificationService::checkSensorValuesAndSendNotifications(float waterValue, float gasValue, float electricityValue) {
  if (waterValue > 1000) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().waterIotDeviceId, NotificationType::Critical, "Water consumption is critically high!");
  } else if (waterValue > 500) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().waterIotDeviceId, NotificationType::Warning, "Water consumption is high. Please check.");
  }

  if (gasValue > 1000) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().gasIotDeviceId, NotificationType::Critical, "Gas consumption is critically high!");
  } else if (gasValue > 500) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().gasIotDeviceId, NotificationType::Warning, "Gas consumption is high. Please check.");
  }

  if (electricityValue > 1000) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().electricityIotDeviceId, NotificationType::Critical, "Electricity consumption is critically high!");
  } else if (electricityValue > 500) {
    sendNotificationToServer(UserSingleton::getInstance().id, ConfigSingleton::getInstance().electricityIotDeviceId, NotificationType::Warning, "Electricity consumption is high. Please check.");
  }
}