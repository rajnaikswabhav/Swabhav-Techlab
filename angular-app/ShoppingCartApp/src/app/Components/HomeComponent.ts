import { Component } from '@angular/core';
import { DataService } from '../Services/DataService';


@Component({
    selector : 'shoppingcart-home',
    templateUrl : 'HomeComponent.html'
})
export class HomeComponent {

    data = [];
    constructor(private userService : DataService){}
    ngOnInit(){

        this.userService.GetData()
            .then(r => this.data = r)
    }

    GetDataById($event){
        let id = $event.target.FirstName;
        console.log(id);
    }
}