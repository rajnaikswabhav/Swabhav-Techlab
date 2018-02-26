import { EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { Component } from '@angular/core';
import { Output } from '@angular/core';


@Component({
    selector : 'ht-star',
    templateUrl : 'StarComponent.html'
})
export class StarComponent {

    @Input()
    rating:number;
    @Output()
    clickRating: EventEmitter<number> = new EventEmitter<number>();
     
   LogRating(){
       //console.log(this.rating);
       this.clickRating.emit(this.rating);
   } 
}