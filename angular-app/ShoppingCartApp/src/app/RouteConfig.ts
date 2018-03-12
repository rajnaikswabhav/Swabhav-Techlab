import { Routes } from "@angular/router";

import { HomeComponent } from "./Components/HomeComponent";
import { LoginComponent } from "./Components/LoginComponent";
import { DashboardComponent } from "./Components/DashboardComponent";





export const ROUTS_ARRAY: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    { path: 'dashboard/:id/home/:name', component: HomeComponent },
    { path: 'dashboard/:id', component: DashboardComponent },
    { path: '', component: LoginComponent, pathMatch: 'full' }
]