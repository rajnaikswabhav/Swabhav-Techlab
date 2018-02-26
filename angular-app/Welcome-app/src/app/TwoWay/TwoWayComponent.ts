import { Promise } from 'q';
import { MathService } from './Service/MathService';
import { Component } from "@angular/core";
import { NumberApiService } from './Service/NumberApiService';


@Component({
    selector: 'ht-twoway',
    templateUrl: 'TwoWayComponent.html'
})
export class TwoWayComponent {
    firstName: string;
    lastName: string;
    company: string;
    numToCheck: number;
    result: number;
    data:any;
    numToGetData:number;
    primeValidationColor = "yellow";
    companies = ["HiTech", "Swabhav", "Fitphelia", "Aurion", "Infosys"];

    constructor(private service: MathService, private httpService: NumberApiService) {
        this.firstName = "Abc";
        this.lastName = "Xyz";
        this.numToCheck;
        this.numToGetData;
    }

    firstNameChange(newName) {
        this.firstName = newName;
    }

    onCheckPrime() {
        if (this.numToCheck == null) {
            this.primeValidationColor = "yellow";
        }
        else {
            this.service.CheckIsNumPrime(this.numToCheck)
                .then(r => {
                    if (r == 0) {
                        this.primeValidationColor = "red";
                    }
                    else {
                        this.primeValidationColor = "green";
                    }
                    this.result = r;
                });
        }

    }

    GetData(){
        console.log("Inside GetData...");
        this.httpService.GetDetail(this.numToGetData)
                        .then((Response:any) => {
                            console.log(Response);
                            this.data = Response._body;
                        })
                        .catch(r => {
                            console.log(r);
                        })
    }
}