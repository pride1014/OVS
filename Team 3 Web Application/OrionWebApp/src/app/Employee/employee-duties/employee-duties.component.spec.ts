import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeDutiesComponent } from './employee-duties.component';

describe('EmployeeDutiesComponent', () => {
  let component: EmployeeDutiesComponent;
  let fixture: ComponentFixture<EmployeeDutiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeDutiesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeDutiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
