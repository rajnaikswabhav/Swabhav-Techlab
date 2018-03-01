import { Routes } from "@angular/router";

import { ProductDetailComponent } from './Components/ProductDetailComponent';
import { WelcomeComponent } from './Components/WelcomeComponent';
import { ProductListComponent } from './Components/ProductListComponent';



export const ROUTES_ARRAY: Routes = [
    { path: 'welcome', component: WelcomeComponent },
    { path: 'productList', component: ProductListComponent },
    { path: 'productDetail/:id ', component: ProductDetailComponent },
    { path: 'productDetail', component: ProductDetailComponent },
    { path: '', component: WelcomeComponent, pathMatch: 'full' }
]