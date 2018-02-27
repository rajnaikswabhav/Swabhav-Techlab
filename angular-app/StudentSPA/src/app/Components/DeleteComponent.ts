import { HomeComponent } from './HomeComponent';
import { Component } from '@angular/core';
import { StudentService } from './../Service/StudentService';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector:'ht-student-home',
    templateUrl:'HomeComponents.html'
})
export class DeleteComponent {

    id:number;

    constructor(private deleteService:StudentService,private route:ActivatedRoute){
        this.route.params.subscribe(params => {
            this.id = params['id'];
        })

        this.DeleteData(this.id);
    }

    DeleteData(studentId){
        this.deleteService.DeleteData(studentId)
            .then(r => {alert("Data Deleted.."+r.status)})
            .catch(r => {console.log(r)});
    }
}