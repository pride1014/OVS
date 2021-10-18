import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductQuoteComponent } from './product-quote.component';

describe('ProductQuoteComponent', () => {
  let component: ProductQuoteComponent;
  let fixture: ComponentFixture<ProductQuoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductQuoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductQuoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
