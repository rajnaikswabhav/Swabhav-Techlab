import { StudentService } from './../Service/StudentService';
import { Component } from '@angular/core';
import { IStudent } from '../IStudent';

@Component({
    selector: 'ht-student-add',
    templateUrl: 'AddStudentComponent.html'
})
export class AddStudentComponent {

    rollNo : number;
    name : string;
    age : number;
    email : string;
    date:string;
    isMale:Boolean;
    gender : Boolean;
    studentObj:IStudent;

    constructor(private addService: StudentService) {
    
    }

    AddStudent(rollNo, name, age, email, date, isMale) {
        console.log(rollNo);
        this.studentObj = {
            rollNo: rollNo,
            name: name,
            age: age,
            email: email,
            date: date,
            isMale: isMale
        };
        console.log(this.studentObj);
        this.addService.AddStudent(this.studentObj)
            .then(r => { alert("Response status: " + r.status) })
            .catch(r => { console.log(r) });

        this.rollNo = null;
        this.name = "";
        this.age = null;
        this.email = "";
        this.date = null;
        this.isMale = true;
    }
}