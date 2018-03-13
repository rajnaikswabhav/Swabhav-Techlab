import { UUID } from 'angular2-uuid';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { ProductService } from '../Services/ProductService';

@Component({
    selector: 'product-home',
    templateUrl: 'ProductDetailComponent.html'
})
export class ProductDetailComponent {

    id: UUID;
    data = [];
    userName: string;
    pname: string;
    pcatagory: string;
    pcost: number;
    discount: number;
    success: string;
    editSuccess:string;
    user : any;


    constructor(private productService: ProductService, private route: ActivatedRoute
        , private router: Router) {
        this.user = JSON.parse(localStorage.getItem('user'));
        this.userName = this.user.FirstName + " " + this.user.LastName;
    }

    ngOnInit() {
        this.id = this.route.snapshot.paramMap.get('id');
        console.log(this.id);
        if (this.id == null) {

        }
        else {
            this.productService.GetByIdProduct(this.id)
                .then(r => {
                    if (r != null) {
                        this.pname = r.ProductName;
                        this.pcatagory = r.ProductCatagory;
                        this.pcost = r.ProductCost;
                        this.discount = r.Discount;
                    }
                })
        }
    }

    Add() {
        this.productService.AddProduct(this.pname, this.pcatagory, this.pcost, this.discount)
            .then(r => {
                console.log(r.status);
                setTimeout(() => {
                    this.success = "Product saved successfully"
                }, 1000);
            })
            .catch(r => { console.log(r) });
        this.pname = "";
        this.pcatagory = "";
        this.pcost = null;
        this.discount = null;
    }

    Edit(){
        this.productService.UpdateProduct(this.id,this.pname,this.pcatagory,this.pcost,this.discount)
            .then(r => {
                alert("Product Data edited:"+r.statusText);
                setTimeout(() => {
                    this.router.navigate(['home',this.user.Id]);
                }, 1000);
            })
            .catch(r => console.log(r));
            this.editSuccess = "Product edit successfully"
    }


}