import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmplyeeLayoutComponent } from './employee-layout.component';

describe('EmplyeeLayoutComponent', () => {
  let component: EmplyeeLayoutComponent;
  let fixture: ComponentFixture<EmplyeeLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmplyeeLayoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmplyeeLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
