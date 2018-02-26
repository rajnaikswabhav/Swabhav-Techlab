import { AboutComponent } from './Components/AboutComponent';
import { Routes } from '@angular/router';
import { HomeComponents } from './Components/HomeComponent';
import { CarrerComponent } from './Components/CarrerComponent';
import { CarrerIdComponent } from './Components/CarrerIdComponent';


export const routesArray : Routes = [
    {
        path :'home',
        component : HomeComponents
    },
    {
        path : 'about',
        component : AboutComponent
    },
    {
        path : 'carrer',
        component : CarrerComponent,
    },
    {
        path : 'carrer/:id',
        component : CarrerIdComponent,
    }
    {
        path : '',
        redirectTo : '/HomeComponents',
        pathMatch : 'full'
    },
];
