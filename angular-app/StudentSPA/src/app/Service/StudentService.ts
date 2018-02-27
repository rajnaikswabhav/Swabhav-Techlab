import { IStudent } from './../IStudent';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';


@Injectable()
export class StudentService {
    API_URL = "http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students";

    constructor (private http : Http){}

    GetData() : Promise<IStudent>{
        return  this.http.get(this.API_URL).toPromise()
        .then(r => {return <IStudent>r.json();
        });
    }

    EditStudentData(studentId,studentObj:IStudent){
        return this.http.put(this.API_URL+'/'+studentId,studentObj).toPromise();
    }

    GetStudentById(studentId){
        return this.http.get(this.API_URL+'/'+studentId).toPromise()
        .then(r => {return <IStudent>r.json()});
    }

    AddStudent(studentObj:IStudent){
        return  this.http.post(this.API_URL,studentObj).toPromise();
    }

    DeleteData(studentId){
        return this.http.delete(this.API_URL+'/'+studentId).toPromise();
    }
}