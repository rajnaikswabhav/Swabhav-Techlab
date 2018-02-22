export class Student {

    constructor(private rollNo, private name){}

    public get Detail() {
        return `rollNo : ${this.rollNo},name : ${this.name}`;
    }

}

// let s = new Student(101,'Akash');
// console.log(s.Detail);