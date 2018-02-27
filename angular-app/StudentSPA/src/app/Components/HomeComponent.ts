import { Output } from '@angular/core';
import { Component } from '@angular/core';
import { StudentService } from "../Service/StudentService";
import { Response } from '@angular/http';
import { EventEmitter } from 'events';
import { EditComponent } from './EditComponent';
import { Router } from '@angular/router';
import { IStudent } from '../IStudent';


@Component({
    selector : 'ht-student-home',
    templateUrl : 'HomeComponents.html'
})
export class HomeComponent {
    data : IStudent ;
    studentId:number;
    editComponent:any;
    constructor(private getService : StudentService,private router:Router){
        
    }

    ngOnInit(){
        console.log("Inside Home....");
        this.getService.GetData()
            .then(r => this.data = r)
    }

    ClearItem(id){
        console.log(id);
        this.getService.DeleteData(id)
            .then(r => alert("Data Deleted: "+r.status))
            .catch(r => {console.log(r)});
        this.ngOnInit();
    }

    EditData(id){
        console.log("EditData...");
        this.router.navigate(['edit',id]);
    }
}