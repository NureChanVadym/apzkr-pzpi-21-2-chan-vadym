/**
 * @file config.h
 * @brief Configuration header file for the IoT resource consumption monitoring system.
 */

#ifndef CONFIG_H
#define CONFIG_H

#include <Arduino.h>
#include <ArduinoJson.h>
#include <SD.h>

/**
 * @brief Structure representing the configuration.
 */
struct Config {
  int ldrWaterPin;           ///< LDR water pin
  int ldrGasPin;             ///< LDR gas pin
  int ldrElectricityPin;     ///< LDR electricity pin
  float gamma;               ///< Gamma value for LDR calibration
  float rl10;                ///< RL10 value for LDR calibration
  String ssid;               ///< Wi-Fi SSID
  String password;           ///< Wi-Fi password
  String serverUrl;          ///< Server URL
  String userLogin;          ///< User login
  String userPassword;       ///< User password
  int waterIotDeviceId;      ///< Water IoT device ID
  int gasIotDeviceId;        ///< Gas IoT device ID
  int electricityIotDeviceId;  ///< Electricity IoT device ID
  int waterTariffId;         ///< Water tariff ID
  int gasTariffId;           ///< Gas tariff ID
  int electricityTariffId;   ///< Electricity tariff ID
  String waterInitialUnit;   ///< Water initial unit
  String gasInitialUnit;     ///< Gas initial unit
  String electricityInitialUnit;  ///< Electricity initial unit
  unsigned long sensorReadInterval;  ///< Sensor read interval
  unsigned long dataSendInterval;    ///< Data send interval
  unsigned long statsInterval;       ///< Stats calculation interval
};

/**
 * @brief Singleton class for managing the configuration.
 */
class ConfigSingleton {
public:
  /**
   * @brief Get the configuration instance.
   * @return Reference to the configuration instance.
   */
  static Config& getInstance() {
    static ConfigSingleton instance;
    return instance.config;
  }

private:
  ConfigSingleton() { loadConfig(); }

  void loadConfig() {
    File configFile = SD.open("/config.json");
    if (configFile) {
      DynamicJsonDocument doc(1024);
      DeserializationError error = deserializeJson(doc, configFile);
      if (error) {
        Serial.println("Failed to parse config file");
        return;
      }
      config.ldrWaterPin = doc["LDR_WATER_PIN"].as<int>();
      config.ldrGasPin = doc["LDR_GAS_PIN"].as<int>();
      config.ldrElectricityPin = doc["LDR_ELECTRICITY_PIN"].as<int>();
      config.gamma = doc["GAMMA"].as<float>();
      config.rl10 = doc["RL10"].as<float>();
      config.ssid = doc["SSID"].as<String>();
      config.password = doc["PASSWORD"].as<String>();
      config.serverUrl = doc["SERVER_URL"].as<String>();
      config.userLogin = doc["USER_LOGIN"].as<String>();
      config.userPassword = doc["USER_PASSWORD"].as<String>();
      config.waterIotDeviceId = doc["WATER_IOT_DEVICE_ID"].as<int>();
      config.gasIotDeviceId = doc["GAS_IOT_DEVICE_ID"].as<int>();
      config.electricityIotDeviceId = doc["ELECTRICITY_IOT_DEVICE_ID"].as<int>();
      config.waterTariffId = doc["WATER_TARIFF_ID"].as<int>();
      config.gasTariffId = doc["GAS_TARIFF_ID"].as<int>();
      config.electricityTariffId = doc["ELECTRICITY_TARIFF_ID"].as<int>();
      config.waterInitialUnit = doc["WATER_INITIAL_UNIT"].as<String>();
      config.gasInitialUnit = doc["GAS_INITIAL_UNIT"].as<String>();
      config.electricityInitialUnit = doc["ELECTRICITY_INITIAL_UNIT"].as<String>();
      config.sensorReadInterval = doc["SENSOR_READ_INTERVAL"].as<unsigned long>();
      config.dataSendInterval = doc["DATA_SEND_INTERVAL"].as<unsigned long>();
      config.statsInterval = doc["STATS_INTERVAL"].as<unsigned long>();
      configFile.close();
      Serial.println("Configuration loaded");
    } else {
      Serial.println("Failed to open config file");
    }
  }

  Config config;  ///< Configuration instance
};

#endif // CONFIG_H