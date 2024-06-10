/**
 * @file utils.cpp
 * @brief Utility functions implementation file for the IoT resource consumption monitoring system.
 */

#include "utils.h"

/**
 * @brief Get the formatted UTC time as a string.
 * @return The formatted UTC time string.
 */
String getFormattedUtcTime() {
  time_t now = time(nullptr);
  struct tm timeinfo;
  gmtime_r(&now, &timeinfo);
  char formattedTime[24];
  strftime(formattedTime, sizeof(formattedTime), "%FT%TZ", &timeinfo);
  return String(formattedTime);
}