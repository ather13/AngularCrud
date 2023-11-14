import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmplyeeAddEditComponent } from './employee-add-edit.component';

describe('EmplyeeAddEditComponent', () => {
  let component: EmplyeeAddEditComponent;
  let fixture: ComponentFixture<EmplyeeAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmplyeeAddEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmplyeeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
