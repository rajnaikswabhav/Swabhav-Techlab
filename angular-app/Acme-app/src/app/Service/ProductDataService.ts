import { Http} from '@angular/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductDataService {

    constructor(private http :Http) {}

    public GetJsonData(){
        return this.http.get("assets/products.json")
                    .map((response : any) => {
                        const data = response.json();
                        return data;
                    });
    }

    public GetDataById(id){
        console.log(id);
        return this.http.get("assets/products.json")
                    .map((response : any) => {
                        const data = response.json();
                        return data;
                    })
                    .filter(data => data.productId == 5);
    }
}