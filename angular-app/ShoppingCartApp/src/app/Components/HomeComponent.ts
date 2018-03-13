import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { UserService } from '../Services/UserService';
import { Session } from 'selenium-webdriver';
import { ProductService } from '../Services/ProductService';


@Component({
    selector: 'shoppingcart-home',
    templateUrl: 'HomeComponent.html'
})
export class HomeComponent {

    data = [];
    userName: string;
    imageUrl:string;
    catagory:string;
    catagaries = ["Electronics","Mobiles","Cloths","Fitness","Kids"];

    constructor(private userService: UserService, private route: ActivatedRoute,
        private router: Router, private productService: ProductService) {
        let user = JSON.parse(localStorage.getItem('user'));
        this.userName = user.FirstName + " " + user.LastName;
        this.imageUrl = "assets/laptopImage.jpg";
    }
    ngOnInit() {

        this.productService.GetAllProducts()
            .then(r => this.data = r)
            .catch(r => alert(r));
    }

    DeleteProduct(id) {
        this.productService.DeleteProduct(id)
            .then(r => {
                alert("Product Deleted");
                this.ngOnInit();
                console.log(this.data.length);
            })
            .catch(r => { console.log(r) });
    }

    EditProduct(id,pname,pcatagory,pcost,discount){
        console.log(id,pname,pcatagory,pcost,discount);
        this.router.navigate(['product',id]);
    }
}