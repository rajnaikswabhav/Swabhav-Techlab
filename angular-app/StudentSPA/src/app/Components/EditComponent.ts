import { ActivatedRoute } from '@angular/router';
import { EventEmitter } from 'events';
import { StudentService } from './../Service/StudentService';
import { Component } from '@angular/core';


@Component({
    selector: 'ht-student-edit',
    templateUrl: 'EditComponent.html'
})
export class EditComponent {

    rollNo: number;
    name: string;
    age: number;
    email: string;
    date: Date;
    isMale: boolean;
    id: number;
    data: any = {};

    constructor(private editService: StudentService, private route: ActivatedRoute) {
        this.route.params.subscribe(params => {
            this.id = params['id'];

            this.editService.GetStudentById(this.id)
                .then((r: any) => {
                    console.log(r._body)
                    this.data = r.json();

                    console.log(this.data.rollNo);
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

    EditStudentData(rollNo,name,age,email,date,isMale){
        let dataObj = {
            rollNo : rollNo,
            name : name,
            age : age,
            email : email,
            date : date,
            isMale : isMale
        }

        this.editService.EditStudentData(this.id,dataObj)
            .then(r => {console.log("Data Edited : "+r.status)})
            .catch(r => {console.log(r)});
        
    }
}