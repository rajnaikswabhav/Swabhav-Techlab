import { Component } from "@angular/core";
import { Router } from '@angular/router';

import { ProductDataService } from './../Service/ProductDataService';


@Component({
    selector : 'acme-productList',
    templateUrl : 'ProductListComponent.html'
})
export class ProductListComponent {
    
    data:any = [];
    FLAG = 0;

    constructor(private productService : ProductDataService,private router:Router) {}

    ngOnInit(){
        this.productService.GetJsonData()
            .subscribe(r => {
                console.log(r)
                this.data = r;
            });
    }

    ShowImage(){
        this.FLAG = 1;
    }

    HideImage(){
        this.FLAG = 0;
    }

    GetDetail(id){
        console.log("Inside GetDetails..."+id);
        this.router.navigate(['productDetail',id]);
    }
}