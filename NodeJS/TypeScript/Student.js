"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Student {
    constructor(rollNo, name) {
        this.rollNo = rollNo;
        this.name = name;
    }
    get Detail() {
        return `rollNo : ${this.rollNo},name : ${this.name}`;
    }
}
exports.Student = Student;
// let s = new Student(101,'Akash');
// console.log(s.Detail);
