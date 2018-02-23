import { Component } from "@angular/core";


@Component({
    selector: 'ht-student',
    templateUrl: 'StudentComponent.html'
})
export class StudentComponent {
    student: any;
    someData: string;
    imageWidth = 100;
    studentCGPA = 8.50;
    color: string;
    courses = [];

    constructor() {
        this.student = {
            rollNo: 101,
            firstName: 'Abc',
            lastName: 'Xyz',
            cgpa: this.studentCGPA,
            profilePic: '../../assets/Images/coniglio_rabbit_small.svg'
        }
        this.someData = "This is some data from the view.";
    }
    public get Color() {
        if (this.studentCGPA > 8) {
           return this.color = "Green";
        }
        else if (this.studentCGPA > 6.50) {
            return this.color = "Yellow";
        }
        else {
            return this.color = "Red";
        }
    }

     LoadCourse($event){
        console.log("Inside LoadCourse..");
        console.log($event);
        this.courses.push ("JAVA","C#","Angular","Android","Ionic","NodeJS");
    }
}