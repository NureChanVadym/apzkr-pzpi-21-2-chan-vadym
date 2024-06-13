/**
 * @file sketch.ino
 * @brief Main file for the IoT resource consumption monitoring system.
 */

#include <Arduino.h>
#include "config.h"
#include "data_collection_service.h"
#include "analytics_service.h"
#include "notification_service.h"
#include "tariff_repository.h"
#include "consumption_repository.h"
#include "user_singleton.h"
#include "sensor_service.h"
#include "authentication_service.h"
#include "utils.h"

float waterConsumption = 0;      ///< Water consumption value
float gasConsumption = 0;        ///< Gas consumption value
float electricityConsumption = 0;  ///< Electricity consumption value

unsigned long lastSensorReadTime = 0;  ///< Timestamp of the last sensor read
unsigned long lastDataSendTime = 0;    ///< Timestamp of the last data send
unsigned long lastStatsTime = 0;       ///< Timestamp of the last stats calculation

std::vector<float> waterStatsConsumptions;        ///< Vector of water consumption values for statistics
std::vector<float> gasStatsConsumptions;          ///< Vector of gas consumption values for statistics
std::vector<float> electricityStatsConsumptions;  ///< Vector of electricity consumption values for statistics

const int CS_PIN = 5; ///< SD card pin connection

/**
 * @brief Setup function for the Arduino sketch.
 */
void setup() {
  Serial.begin(115200);

  if (!SD.begin(CS_PIN)) {
    Serial.println("Card initialization failed!");
    while (true);
  }
  Serial.println("initialization done.");

  Config& config = ConfigSingleton::getInstance();

  pinMode(config.ldrWaterPin, INPUT);
  pinMode(config.ldrGasPin, INPUT);
  pinMode(config.ldrElectricityPin, INPUT);

  WiFi.begin(config.ssid.c_str(), config.password.c_str());
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");

  configTime(0, 0, "pool.ntp.org");
  while (!time(nullptr)) {
    Serial.print(".");
    delay(1000);
  }
  Serial.println("\nTime synchronized");

  if (authenticateUser()) {
    Serial.println("User authenticated");
  } else {
    Serial.println("User authentication failed");
  }
}

/**
 * @brief Main loop function for the Arduino sketch.
 */
void loop() {
  Config& config = ConfigSingleton::getInstance();

  DataCollectionService dataCollectionService;
  AnalyticsService analyticsService;
  NotificationService notificationService;

  TariffRepository tariffRepository;
  ConsumptionRepository consumptionRepository;

  unsigned long currentTime = millis();

  if (currentTime - lastSensorReadTime >= config.sensorReadInterval) {
    lastSensorReadTime = currentTime;

    float waterValue = readLux(config.ldrWaterPin);
    float gasValue = readLux(config.ldrGasPin);
    float electricityValue = readLux(config.ldrElectricityPin);

    Serial.println("--------------------------------------------------");
    Serial.println("Sensor values read:");
    Serial.print("Water: ");
    Serial.println(waterValue);
    Serial.print("Gas: ");
    Serial.println(gasValue);
    Serial.print("Electricity: ");
    Serial.println(electricityValue);
    Serial.println("--------------------------------------------------");

    dataCollectionService.sendSensorDataToServer(waterValue, config.waterIotDeviceId);
    dataCollectionService.sendSensorDataToServer(gasValue, config.gasIotDeviceId);
    dataCollectionService.sendSensorDataToServer(electricityValue, config.electricityIotDeviceId);

    notificationService.checkSensorValuesAndSendNotifications(waterValue, gasValue, electricityValue);

    waterConsumption += waterValue;
    gasConsumption += gasValue;
    electricityConsumption += electricityValue;

    waterStatsConsumptions.emplace_back(waterValue);
    gasStatsConsumptions.emplace_back(gasValue);
    electricityStatsConsumptions.emplace_back(electricityValue);
  }

  if (currentTime - lastDataSendTime >= config.dataSendInterval) {
    lastDataSendTime = currentTime;

    float convertedWaterConsumption = consumptionRepository.convertConsumptionUnit(waterConsumption, config.waterTariffId, ResourceType::Water, config.waterInitialUnit);
    float convertedGasConsumption = consumptionRepository.convertConsumptionUnit(gasConsumption, config.gasTariffId, ResourceType::Gas, config.gasInitialUnit);
    float convertedElectricityConsumption = consumptionRepository.convertConsumptionUnit(electricityConsumption, config.electricityTariffId, ResourceType::Electricity, config.electricityInitialUnit);

    Serial.println("--------------------------------------------------");
    Serial.println("Sending consumption data to server:");
    Serial.print("Water: ");
    Serial.println(convertedWaterConsumption);
    Serial.print("Gas: ");
    Serial.println(convertedGasConsumption);
    Serial.print("Electricity: ");
    Serial.println(convertedElectricityConsumption);
    Serial.println("--------------------------------------------------");

    dataCollectionService.sendConsumptionToServer(convertedWaterConsumption, config.waterTariffId);
    dataCollectionService.sendConsumptionToServer(convertedGasConsumption, config.gasTariffId);
    dataCollectionService.sendConsumptionToServer(convertedElectricityConsumption, config.electricityTariffId);

    waterConsumption = 0;
    gasConsumption = 0;
    electricityConsumption = 0;
  }

  if (currentTime - lastStatsTime >= config.statsInterval) {
    lastStatsTime = currentTime;

    ConsumptionStats waterStats = analyticsService.calculateConsumptionStats(waterStatsConsumptions);
    ConsumptionStats gasStats = analyticsService.calculateConsumptionStats(gasStatsConsumptions);
    ConsumptionStats electricityStats = analyticsService.calculateConsumptionStats(electricityStatsConsumptions);

    String waterStatsMessage = analyticsService.formatStatsMessage(waterStats, "Water");
    String gasStatsMessage = analyticsService.formatStatsMessage(gasStats, "Gas");
    String electricityStatsMessage = analyticsService.formatStatsMessage(electricityStats, "Electricity");

    notificationService.sendStatsToServer(waterStatsMessage, config.waterIotDeviceId);
    notificationService.sendStatsToServer(gasStatsMessage, config.gasIotDeviceId);
    notificationService.sendStatsToServer(electricityStatsMessage, config.electricityIotDeviceId);

    waterStatsConsumptions.clear();
    gasStatsConsumptions.clear();
    electricityStatsConsumptions.clear();
  }
}