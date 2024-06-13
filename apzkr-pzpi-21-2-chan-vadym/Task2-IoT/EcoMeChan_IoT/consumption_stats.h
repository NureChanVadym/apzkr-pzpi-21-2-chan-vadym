/**
 * @file consumption_stats.h
 * @brief Consumption statistics header file for the IoT resource consumption monitoring system.
 */

#ifndef CONSUMPTION_STATS_H
#define CONSUMPTION_STATS_H

/**
 * @brief Structure representing consumption statistics.
 */
struct ConsumptionStats {
  float average;   ///< Average consumption value
  float min;       ///< Minimum consumption value
  float max;       ///< Maximum consumption value
  float stdDev;    ///< Standard deviation of consumption values
};

#endif // CONSUMPTION_STATS_H