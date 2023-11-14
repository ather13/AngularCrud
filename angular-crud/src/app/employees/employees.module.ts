import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee.routing';
import { EmployeeLayoutComponent } from './employee-layout/employee-layout.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeAddEditComponent } from './employee-add-edit/employee-add-edit.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';


@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        EmployeeRoutingModule
    ],
    declarations: [
        EmployeeLayoutComponent,
        EmployeeListComponent,
        EmployeeDetailsComponent,
        EmployeeAddEditComponent
    ]
})
export class EmployeeModule { }