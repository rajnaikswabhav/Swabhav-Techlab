import { UUID } from 'angular2-uuid';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../Services/UserService';
import { Session } from 'selenium-webdriver';

@Component({
    selector: 'admin-dashboard',
    templateUrl: 'DashboardComponent.html'
})
export class DashboardComponent {

    id:UUID;
    name:string;
    userData = {};

    constructor(private userService : UserService,private routes : ActivatedRoute,
    private router : Router){
        this.id = this.routes.snapshot.paramMap.get('id');
    }

    ngOnInit(){
        this.userService.GetDataById(this.id)
            .then(r => {
                this.userData = r;
                this.name = r.FirstName+" "+r.LastName;
                Session['user'] = this.name;
            })
            .catch(r => {console.log(r);});
        
    }

    GoToHome(){
        console.log("Inside h");
        this.router.navigate(['home',this.name]);
    }
}