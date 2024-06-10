/**
 * @file user_singleton.h
 * @brief User singleton header file for the IoT resource consumption monitoring system.
 */

#ifndef USER_SINGLETON_H
#define USER_SINGLETON_H

#include <Arduino.h>
#include <ArduinoJson.h>

/**
 * @brief Structure representing a user.
 */
struct User {
  int id;           ///< User ID
  String login;     ///< User login
  String phone;     ///< User phone number
  String email;     ///< User email
  String lastName;  ///< User last name
  String firstName;   ///< User first name
  String middleName;  ///< User middle name
  String role;        ///< User role
};

/**
 * @brief Singleton class for managing the user instance.
 */
class UserSingleton {
public:
  /**
   * @brief Get the user instance.
   * @return Reference to the user instance.
   */
  static User& getInstance() {
    static UserSingleton instance;
    return instance.user;
  }

  /**
   * @brief Set the user data from a JSON object.
   * @param userData The JSON object containing user data.
   */
  static void setUserData(const JsonObject& userData) {
    User& instance = getInstance();
    instance.id = userData["id"].as<int>();
    instance.login = userData["login"].as<String>();
    instance.phone = userData["phone"].as<String>();
    instance.email = userData["email"].as<String>();
    instance.lastName = userData["lastName"].as<String>();
    instance.firstName = userData["firstName"].as<String>();
    instance.middleName = userData["middleName"].as<String>();
    instance.role = userData["role"].as<String>();
  }

private:
  UserSingleton() {}  ///< Private constructor to prevent instantiation

  User user;  ///< User instance
};

#endif // USER_SINGLETON_H