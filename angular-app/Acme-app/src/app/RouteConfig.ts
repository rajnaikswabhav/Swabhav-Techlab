import { WelcomeComponent } from './Components/WelcomeComponent';
import { Routes } from "@angular/router";
import { ProductListComponent } from './Components/ProductListComponent';



export const routesArray : Routes = [
    {path : 'welcome' ,component: WelcomeComponent},
    {path : 'productList',component:ProductListComponent}
    { path : '' ,redirectTo:'WelcomeComponent',pathMatch:'full'}
]