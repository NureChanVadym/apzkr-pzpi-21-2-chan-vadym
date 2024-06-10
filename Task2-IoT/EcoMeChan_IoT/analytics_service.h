/**
 * @file analytics_service.h
 * @brief Analytics service header file for the IoT resource consumption monitoring system.
 */

#ifndef ANALYTICS_SERVICE_H
#define ANALYTICS_SERVICE_H

#include <Arduino.h>
#include <vector>
#include "consumption_stats.h"

/**
 * @brief Class for performing analytics on consumption data.
 */
class AnalyticsService {
public:
  /**
   * @brief Calculate consumption statistics.
   * @param consumption The vector of consumption values.
   * @return The calculated consumption statistics.
   */
  ConsumptionStats calculateConsumptionStats(const std::vector<float>& consumption);

  /**
   * @brief Format the consumption statistics as a message string.
   * @param stats The consumption statistics.
   * @param resourceName The name of the resource.
   * @return The formatted statistics message.
   */
  String formatStatsMessage(const ConsumptionStats& stats, const String& resourceName);
};

#endif // ANALYTICS_SERVICE_H