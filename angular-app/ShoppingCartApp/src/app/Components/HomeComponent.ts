import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { UserService } from '../Services/UserService';
import { Session } from 'selenium-webdriver';
import { ProductService } from '../Services/ProductService';


@Component({
    selector : 'shoppingcart-home',
    templateUrl : 'HomeComponent.html'
})
export class HomeComponent {

    data = [];
    userName:string;

    constructor(private userService : UserService,private route : ActivatedRoute,
    private router : Router,private productService : ProductService){
        let user =JSON.parse(localStorage.getItem('user'));
        this.userName = user.FirstName+" "+user.LastName ;
    }
    ngOnInit(){

        this.productService.GetAllProducts()
            .then(r => this.data = r)
            .catch(r => alert(r))
    }
}