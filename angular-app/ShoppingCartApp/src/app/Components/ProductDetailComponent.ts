import { Component } from '@angular/core';
import { ProductService } from '../Services/ProductService';

@Component({
    selector: 'product-home',
    templateUrl : 'ProductDetailComponent.html'
})
export class ProductDetailComponent {

    data = [];
    userName:string;
    pname:string;
    pcatagory:string;
    pcost:number;
    discount:number;

    constructor(private productService : ProductService){
        let user =JSON.parse(localStorage.getItem('user'));
        this.userName = user.FirstName+" "+user.LastName ;
    }

    Add(){
    this.productService.AddProduct(this.pname,this.pcatagory,this.pcost,this.discount)
    .then(r => {console.log(r.status)})
    .catch(r => {console.log(r)});        
    }
}