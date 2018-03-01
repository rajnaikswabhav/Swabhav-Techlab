import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';

import { EventEmitter } from 'events';

import { StudentService } from './../Service/StudentService';
import { IStudent } from '../IStudent';
import { Router } from '@angular/router';


@Component({
    selector: 'ht-student-edit',
    templateUrl: 'EditComponent.html'
})
export class EditComponent {

    rollNo: number;
    name: string;
    age: number;
    email: string;
    date: string;
    isMale: Boolean;
    id: number;
    data: IStudent;

    constructor(private editService: StudentService, private route: ActivatedRoute,private router : Router) {
        this.route.params.subscribe(params => {
            this.id = params['id'];

            this.editService.GetStudentById(this.id)
                .then((r: any) => {
                    this.data = r;
                    this.rollNo = this.data.rollNo;
                    this.name = this.data.name;
                    this.age = this.data.age;
                    this.email = this.data.email;
                    this.date = this.data.date;
                    this.isMale = this.data.isMale;
                })
                .catch(r => console.log(r));
        });
    }

    EditStudentData(rollNo, name, age, email, date, isMale) {
        this.data = {
            rollNo: rollNo,
            name: name,
            age: age,
            email: email,
            date: date,
            isMale: isMale
        }

        this.editService.EditStudentData(this.id, this.data)
            .then(r => {
                alert("Data Edited: "+r.status); 
                this.router.navigateByUrl("home") })
            .catch(r => { console.log(r) });

    }
}