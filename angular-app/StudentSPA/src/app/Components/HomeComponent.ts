import { Component } from '@angular/core';
import { StudentService } from "../Service/StudentService";
import { Response } from '@angular/http';


@Component({
    selector : 'ht-student-home',
    templateUrl : 'HomeComponents.html'
})
export class HomeComponent {
    data : any ={} ;
    constructor(private getService : StudentService){
        
    }

    ngOnInit(){
        console.log("Inside Home....");
        this.getService.GetData()
            .then(r => this.data = r.json())
    }


}