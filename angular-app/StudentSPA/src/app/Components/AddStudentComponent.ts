import { StudentService } from './../Service/StudentService';
import { Component } from '@angular/core';

@Component({
    selector: 'ht-student-add',
    templateUrl: 'AddStudentComponent.html'
})
export class AddStudentComponent {

    rollNo : number;
    name : string;
    age : number;
    email : string;
    date:Date;
    isMale:boolean;
    constructor(private addService: StudentService) {
    }

    AddStudent(rollNo, name, age, email, date, isMale) {
        console.log(rollNo);
        let dataObj = {
            rollNo: rollNo,
            name: name,
            age: age,
            email: email,
            date: date,
            isMale: isMale
        };

        this.addService.AddStudent(dataObj)
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