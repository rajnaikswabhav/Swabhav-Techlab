import { Http } from "@angular/http";
import { Injectable } from "@angular/core";


@Injectable()
export class NumberApiService {

    constructor(private _http : Http){}

    GetDetail(numToGetData){
        return this._http.get("http://numbersapi.com/"+numToGetData).toPromise();
    }
}