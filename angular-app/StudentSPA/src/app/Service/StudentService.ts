import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class StudentService {
    API_URL = "http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students";
    constructor (private http : Http){}

    GetData(){
        return this.http.get(this.API_URL).toPromise();
    }

    EditStudentData(studentId,dataObj){
        return this.http.put(this.API_URL+'/'+studentId,dataObj);
    }

    GetStudentById(studentId){
        return this.http.get(this.API_URL+'/'+studentId).toPromise();
    }

    AddStudent(dataObj){
        return this.http.post(this.API_URL,dataObj).toPromise();
    }
}