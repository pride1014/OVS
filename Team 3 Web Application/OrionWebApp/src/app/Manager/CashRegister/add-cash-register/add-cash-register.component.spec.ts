import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCashRegisterComponent } from './add-cash-register.component';

describe('AddCashRegisterComponent', () => {
  let component: AddCashRegisterComponent;
  let fixture: ComponentFixture<AddCashRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCashRegisterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCashRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
