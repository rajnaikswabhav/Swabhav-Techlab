import { Component } from '@angular/core';

@Component({
    selector : 'ht-binding',
    templateUrl : 'BindingComponent.html'
})
export class BindingComponent {
    colorNames = [];
    color:string;
    bgColor:string;
    fontSize:number ;
    message:string;
    constructor(){
        this.colorNames = ["Red","blue","green","Yellow","Pink"];
        this.message = "";
    }
}