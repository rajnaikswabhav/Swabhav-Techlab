import { AddStudentComponent } from './Components/AddStudentComponent';
import { HomeComponent } from './Components/HomeComponent';
import { Routes } from '@angular/router';

export const routesArray : Routes = [
    {path : 'home' , component : HomeComponent},
    {path : 'Add Student',component: AddStudentComponent}
    //  {path : 'edit/:id' , component : },
    // {path : 'delete/:id' , component : DeleteComponent},
    // {path : '' , component : '/HomeComponent',pathMatch : 'full'}
]