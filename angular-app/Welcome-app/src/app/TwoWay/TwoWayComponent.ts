import { Component } from "@angular/core";


@Component({
    selector : 'ht-twoway',
    templateUrl : 'TwoWayComponent.html'
})
export class TwoWayComponent {
    firstName:string;
    lastName:string;
    company:string;
    companies = ["HiTech","Swabhav","Fitphelia","Aurion","Infosys"];

    constructor(){
        this.firstName = "Abc";
        this.lastName = "Xyz";
    }

    firstNameChange(newName){
        this.firstName = newName;
 
    }
}