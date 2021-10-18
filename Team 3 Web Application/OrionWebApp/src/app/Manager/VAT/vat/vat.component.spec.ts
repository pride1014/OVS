import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VATComponent } from './vat.component';

describe('VATComponent', () => {
  let component: VATComponent;
  let fixture: ComponentFixture<VATComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VATComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VATComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
