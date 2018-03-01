import { ProductDataService } from './../Service/ProductDataService';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'acme-detail',
    templateUrl: 'ProductDetailComponent.html'
})
export class ProductDetailComponent {

    id: number;
    data:any ;
    
    constructor(private route: ActivatedRoute,private getService : ProductDataService) {
        this.route.params.subscribe(params => { this.id = params['id']; })
        console.log("id:" +this.id);
        this.GetDetails();
    }

    GetDetails(){
        this.getService.GetJsonData()
            .subscribe(r => {
                console.log(r);
            })
    }
}