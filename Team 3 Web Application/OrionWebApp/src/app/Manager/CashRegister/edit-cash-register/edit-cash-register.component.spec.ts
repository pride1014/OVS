import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCashRegisterComponent } from './edit-cash-register.component';

describe('EditCashRegisterComponent', () => {
  let component: EditCashRegisterComponent;
  let fixture: ComponentFixture<EditCashRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditCashRegisterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCashRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
