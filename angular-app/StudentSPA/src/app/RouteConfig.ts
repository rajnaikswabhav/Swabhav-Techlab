import { Routes } from '@angular/router';

import { DeleteComponent } from './Components/DeleteComponent';
import { AddStudentComponent } from './Components/AddStudentComponent';
import { HomeComponent } from './Components/HomeComponent';
import { EditComponent } from './Components/EditComponent';

export const ROUTS_ARRAY: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'Add Student', component: AddStudentComponent },
    { path: 'edit', component: EditComponent },
    { path: 'edit/:id', component: EditComponent },
    { path: 'delete/:id', component: DeleteComponent },
    { path: '', component: HomeComponent, pathMatch: 'full' }
]