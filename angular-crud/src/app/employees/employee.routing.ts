import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EmployeeLayoutComponent } from './employee-layout/employee-layout.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeAddEditComponent } from './employee-add-edit/employee-add-edit.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';

const routes: Routes = [
    {
        path: '', component: EmployeeLayoutComponent,
        children: [
            { path: '', component: EmployeeListComponent },
            { path: 'add', component: EmployeeAddEditComponent },
            { path: 'edit/:id', component: EmployeeAddEditComponent },
            { path: 'details', component: EmployeeDetailsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EmployeeRoutingModule { }