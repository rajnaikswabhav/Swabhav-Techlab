import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Contact, Contacts } from '@ionic-native/contacts';

@Component({
    selector:'ht-namematch',
    templateUrl:'NameMatchingPage.html'
})
export class NameMatch {

    name1 : string;
    name2 : string;
    result : string;
    constructor(private navCtr : NavController,private contactService : Contacts){
    }

    SelectPerson1(){
        this.contactService.pickContact()
            .then(s => this.name1 = s.displayName)
            .catch(e => console.log(e));
    }

    SelectPerson2(){
        this.contactService.pickContact()
            .then(s => this.Calculate(s.displayName))
            .catch(e => console.log(e));
    }



    Calculate(newName){
        console.log(newName);
        this.name2 = newName;
        let sum : number = 0;

        for(let i = 0; i<this.name1.length; i++){
            sum = sum + this.name1.toUpperCase().charCodeAt(i);
        }
        console.log(sum);
        for(let i = 0; i<this.name2.length; i++){
            sum = sum + this.name2.toUpperCase().charCodeAt(i);
        }
        console.log(sum);
         this.result = sum%101 +" %";
    }

}