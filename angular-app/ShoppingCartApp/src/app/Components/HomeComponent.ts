import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { UserService } from '../Services/UserService';
import { Session } from 'selenium-webdriver';


@Component({
    selector : 'shoppingcart-home',
    templateUrl : 'HomeComponent.html'
})
export class HomeComponent {

    data = [];
    userName:string;

    constructor(private userService : UserService,private route : ActivatedRoute){
        this.userName = Session['user'];
    }
    ngOnInit(){

        this.userService.GetData()
            .then(r => this.data = r)
            .catch(r => alert(r))
    }

    GetDataById($event){
        let id = $event.target.FirstName;
        console.log(id);
    }
}