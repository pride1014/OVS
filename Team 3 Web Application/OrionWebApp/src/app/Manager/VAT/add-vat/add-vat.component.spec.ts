import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVATComponent } from './add-vat.component';

describe('AddVATComponent', () => {
  let component: AddVATComponent;
  let fixture: ComponentFixture<AddVATComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddVATComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddVATComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
