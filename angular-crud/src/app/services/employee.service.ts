import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../models/employee.model';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

const baseUrl = `${environment.apiUrl}/employee`;

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
   
  constructor(private http: HttpClient) { }

  getAll = ():Observable<Employee[]> => {
      return this.http.get<Employee[]>(baseUrl);
  }

  getById = (id: number):Observable<Employee> => {
      return this.http.get<Employee>(`${baseUrl}/${id}`);
  }

  create = (params: Employee) => {
      return this.http.post(baseUrl, params);
  }

  update = (id: string, params: Employee) => {
      return this.http.put(`${baseUrl}/${id}`, params);
  }

  delete = (id: number) => {
      return this.http.delete(`${baseUrl}/${id}`);
  }
}
