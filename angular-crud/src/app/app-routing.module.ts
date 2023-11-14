import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './errorpages/pagenotfound/pagenotfound.component';


const employeeModule = () => import('./employees/employees.module').then(x => x.EmployeeModule);

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'employees', loadChildren: employeeModule },

    // otherwise redirect to home
    { path: '**', component:PageNotFoundComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
