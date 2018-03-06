

export class AuthService {

    AuthticateUser(userName, password) {
        if (userName == "admin" && password == "admin") {
            return true;
        }
        else {
            return false;
        }
    }
}