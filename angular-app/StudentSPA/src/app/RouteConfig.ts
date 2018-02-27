import { DeleteComponent } from './Components/DeleteComponent';
import { AddStudentComponent } from './Components/AddStudentComponent';
import { HomeComponent } from './Components/HomeComponent';
import { Routes } from '@angular/router';
import { EditComponent } from './Components/EditComponent';

export const Routes_Array: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'Add Student', component: AddStudentComponent },
    { path: 'edit', component: EditComponent },
    { path: 'edit/:id', component: EditComponent },
    { path: 'delete/:id', component: DeleteComponent },
    { path: '', component: HomeComponent, pathMatch: 'full' }
]