import { Http } from "@angular/http";
import { Injectable } from "@angular/core";



@Injectable()
export class DataService {
    API_URL = "http://localhost:49299/api/v1/ShoppingCart/User";   

    constructor(private http:Http){}

    GetData() {
        return  this.http.get(this.API_URL+"/GetAllUsers").toPromise()
        .then(r => {return r.json();
        });
    }
}