import { Component, Output } from '@angular/core';
import { Response } from '@angular/http';
import { Router } from '@angular/router';

import { StudentService } from "../Service/StudentService";
import { EditComponent } from './EditComponent';
import { IStudent } from '../IStudent';


import { EventEmitter } from 'events';


@Component({
    selector: 'ht-student-home',
    templateUrl: 'HomeComponents.html'
})
export class HomeComponent {
    data: IStudent;
    studentId: number;
    editComponent: any;
    constructor(private getService: StudentService, private router: Router) {

    }

    ngOnInit() {
        this.getService.GetData()
            .then(r => this.data = r)
    }

    ClearItem(id) {
        this.getService.DeleteData(id)
            .then(r => {
                alert("Data Deleted: " + r.status);
                this.ngOnInit();
            })
            .catch(r => { console.log(r) });

    }

    EditData(id) {
        this.router.navigate(['edit', id]);
    }
}