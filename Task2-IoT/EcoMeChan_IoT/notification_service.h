/**
 * @file notification_service.h
 * @brief Notification service header file for the IoT resource consumption monitoring system.
 */

#ifndef NOTIFICATION_SERVICE_H
#define NOTIFICATION_SERVICE_H

#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "user_singleton.h"
#include "notification_type.h"

/**
 * @brief Class for sending notifications to the server.
 */
class NotificationService {
public:
  /**
   * @brief Send a notification to the server.
   * @param userId The user ID.
   * @param iotDeviceId The IoT device ID.
   * @param notificationType The notification type.
   * @param text The notification text.
   */
  void sendNotificationToServer(int userId, int iotDeviceId, NotificationType notificationType, const String& text);

  /**
   * @brief Send consumption statistics to the server.
   * @param statsText The statistics text.
   * @param deviceId The IoT device ID.
   */
  void sendStatsToServer(const String& statsText, int deviceId);

  /**
   * @brief Check sensor values and send notifications based on thresholds.
   * @param waterValue The water sensor value.
   * @param gasValue The gas sensor value.
   * @param electricityValue The electricity sensor value.
   */
  void checkSensorValuesAndSendNotifications(float waterValue, float gasValue, float electricityValue);
};

#endif // NOTIFICATION_SERVICE_H