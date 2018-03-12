import { Routes } from "@angular/router";

import { HomeComponent } from "./Components/HomeComponent";
import { LoginComponent } from "./Components/LoginComponent";
import { ProductDetailComponent } from "./Components/ProductDetailComponent";





export const ROUTS_ARRAY: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    { path: 'home/:id', component: HomeComponent },
    { path: 'home/:id/product', component: ProductDetailComponent},
    { path: '', component: LoginComponent, pathMatch: 'full' }
]