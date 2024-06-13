// src/models/User.js
import Role from '../enums/Role';

class User {
    constructor(id, login, phone, email, lastName, firstName, middleName, role) {
        this.id = id;
        this.login = login;
        this.phone = phone;
        this.email = email;
        this.lastName = lastName;
        this.firstName = firstName;
        this.middleName = middleName;
        this.role = role;
    }
}

export default User;