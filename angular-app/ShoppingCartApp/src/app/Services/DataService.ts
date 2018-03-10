import { Http } from "@angular/http";
import { Injectable } from "@angular/core";



@Injectable()
export class DataService {
    USER_URL = "http://localhost:49299/api/v1/ShoppingCart/User"; 

    constructor(private http:Http){}

    GetData() {
        return  this.http.get(this.USER_URL+"/GetAllUsers").toPromise()
        .then(r => {return r.json();
        });
    }

    RegisterAdmin(adminUser){
        return this.http.post(this.USER_URL+"/AddUser",adminUser).toPromise();
    }
}