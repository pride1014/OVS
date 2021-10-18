import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProductSizeComponent } from './add-product-size.component';

describe('AddProductSizeComponent', () => {
  let component: AddProductSizeComponent;
  let fixture: ComponentFixture<AddProductSizeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProductSizeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProductSizeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
