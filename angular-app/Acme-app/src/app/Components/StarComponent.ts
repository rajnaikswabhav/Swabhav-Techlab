import { Component, Input, Output,EventEmitter } from '@angular/core';


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