/**
 * @file analytics_service.cpp
 * @brief Analytics service implementation file for the IoT resource consumption monitoring system.
 */

#include "analytics_service.h"

/**
 * @brief Calculate consumption statistics.
 * @param consumption The vector of consumption values.
 * @return The calculated consumption statistics.
 */
ConsumptionStats AnalyticsService::calculateConsumptionStats(const std::vector<float>& consumption) {
  float sum = 0;
  float min = consumption.empty() ? 0 : consumption[0];
  float max = consumption.empty() ? 0 : consumption[0];

  for (float value : consumption) {
    sum += value;
    if (value < min) {
      min = value;
    }
    if (value > max) {
      max = value;
    }
  }

  float average = consumption.empty() ? 0 : sum / consumption.size();

  float sumSquaredDiff = 0;
  for (float value : consumption) {
    float diff = value - average;
    sumSquaredDiff += diff * diff;
  }

  float variance = consumption.size() <= 1 ? 0 : sumSquaredDiff / (consumption.size() - 1);
  float stdDev = sqrt(variance);

  ConsumptionStats stats = { average, min, max, stdDev };
  return stats;
}

/**
 * @brief Format the consumption statistics as a message string.
 * @param stats The consumption statistics.
 * @param resourceName The name of the resource.
 * @return The formatted statistics message.
 */
String AnalyticsService::formatStatsMessage(const ConsumptionStats& stats, const String& resourceName) {
  String message = resourceName + " Consumption Stats:\n";
  message += "Average: " + String(stats.average) + "\n";
  message += "Min: " + String(stats.min) + "\n";
  message += "Max: " + String(stats.max) + "\n";
  message += "Standard Deviation: " + String(stats.stdDev) + "\n";
  return message;
}