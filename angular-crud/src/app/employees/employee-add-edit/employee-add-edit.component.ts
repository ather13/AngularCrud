import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime } from 'rxjs';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-add-edit',
  templateUrl: './employee-add-edit.component.html',
  styleUrl: './employee-add-edit.component.scss'
})
export class EmployeeAddEditComponent {
  form!: FormGroup;
  id?: string;
  title!: string;
  loading = false;
  submitting = false;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private employeeService: EmployeeService,
      //private alertService: AlertService
  ) {}

  ngOnInit() {
      this.id = this.route.snapshot.params['id'];
      
      this.form = this.formBuilder.group({
          name: ['', Validators.required],
          phone: ['',  [Validators.required,Validators.pattern(/^\d{3}-\d{3}-\d{4}$/)]],
          address: ['', Validators.required],
          city: ['', Validators.required],
          region: ['', Validators.required],
          postalCode:['', Validators.required],
          country:['', Validators.required]
      }
      );

      this.title = 'Add employee';
      if (this.id) {
          // edit mode
          this.title = 'Edit employee';
          this.loading = true;
          this.employeeService.getById(Number.parseInt(this.id))
              .subscribe({
               next: (x) => {
                  console.log(x);
                  this.form.patchValue(x);
                  this.loading = false;
              },
              error: (e) => {
                console.log(`Error in getting data ${JSON.stringify(e)}`);
              }
            });
      }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;

      // reset alerts on submit
      //this.alertService.clear();

      // stop here if form is invalid
      if (this.form.invalid) {
          return;
      }

      this.submitting = true;
      this.saveemployee()
          .subscribe({
              next: () => {
                alert('Data saved successfully');
//this.alertService.success('employee saved', { keepAfterRouteChange: true });
                console.log('Data saved successfully');
                  this.router.navigateByUrl('/employees');
              },
              error: (error:any) => {
  //                this.alertService.error(error);
  console.log('Data saved successfully');
  alert('error in saving data');
                  this.submitting = false;
              }
          })
  }

  private saveemployee() {
      // create or update employee based on id param
      return this.id
          ? this.employeeService.update(this.id!, this.form.value)
          : this.employeeService.create(this.form.value);
  }
}
