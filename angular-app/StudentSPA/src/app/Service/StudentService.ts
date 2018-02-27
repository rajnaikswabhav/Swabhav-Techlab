import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { IStudent} from '../IStudetnt';

@Injectable()
export class StudentService {
    API_URL = "http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students";
    constructor (private http : Http){}

    GetData(){
        return this.http.get(this.API_URL).toPromise();
    }

    EditStudentData(studentId,studentObj:IStudent){
        return this.http.put(this.API_URL+'/'+studentId,studentObj).toPromise();
    }

    GetStudentById(studentId){
        return this.http.get(this.API_URL+'/'+studentId).toPromise();
    }

    AddStudent(studentObj:IStudent){
        return this.http.post(this.API_URL,studentObj).toPromise();
    }

    DeleteData(studentId){
        return this.http.delete(this.API_URL+'/'+studentId).toPromise();
    }
}