import { StudentService } from './../Service/StudentService';
import { Component } from '@angular/core';

@Component({
    selector : 'ht-student-edit',
    templateUrl : 'EditComponent.html'
})
export class EditComponent {

    studentId:number;
    constructor (private editService : StudentService){
        
    }

    // ngOnInit(){
    //     this.editService.EditStudentData(studentId,dataObj)
    // }
}