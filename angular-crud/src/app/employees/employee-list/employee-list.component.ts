import { Component } from '@angular/core';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent {
  employees?: Employee[];

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
      this.load();
  }

  load = () => {
    this.employeeService.getAll()
    .subscribe({
      next:(emp) => { 
        this.employees = emp
        console.log(emp);
      }
      ,error: (err) => { 
        this.employees = [];
        console.log(`Error in loading employees : ${JSON.stringify(err)}`);
      }
    });
    
  }

  removeEmployee = (id: number) => {
      const emp = this.employees!.find(x => x.employeeId === id);
      
      this.employeeService.delete(id)
          .subscribe({
           next: () => this.employees = this.employees!.filter(x => x.employeeId !== id)
           ,error: (err) => { 
            console.log(`Error in deleting employees : ${JSON.stringify(err)}`);
          }
          });
  }
}
