
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector : 'ht-carrerid',
    templateUrl : 'CarrerIdComponent.html'
})
export class CarrerIdComponent {
    id:number;
    constructor(private route:ActivatedRoute){

    }

    ngOnInit(){
        this.route.params.subscribe(params => {
            this.id = params['id'];
        })
    }
}