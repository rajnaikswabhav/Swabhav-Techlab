import { UUID } from 'angular2-uuid';
import { Injectable } from "@angular/core";
import { Http } from "@angular/http";



@Injectable()
export class ProductService {

    PRODUCT_URL = "http://localhost:49299/api/v1/ShoppingCart/Product";
    
    constructor(private http : Http){}

    GetAllProducts(){
        return this.http.get(this.PRODUCT_URL+"/GetAllProduct").toPromise()
                    .then(r => {return r.json();})
    }

    GetByIdProduct(id){
        return this.http.get(this.PRODUCT_URL+"/GetProduct/"+id).toPromise()
                    .then(r => {return  r.json();});
    }

    AddProduct(pname,pcatagory,pcost,discount){
        let newProduct = {
            Id : UUID.UUID(),
            ProductName : pname,
            ProductCatagory : pcatagory,
            ProductCost : pcost,
            Discount : discount
        }

        return this.http.post(this.PRODUCT_URL+"/AddProduct",newProduct).toPromise();
    }

    UpdateProduct(id){
        let oldProduct ;
        this.GetByIdProduct(id)
        .then(r => oldProduct = r)
        .catch(r => {console.log(r)});
    }

}