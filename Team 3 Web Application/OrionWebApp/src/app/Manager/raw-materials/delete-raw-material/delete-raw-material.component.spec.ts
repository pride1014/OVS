import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRawMaterialComponent } from './delete-raw-material.component';

describe('DeleteRawMaterialComponent', () => {
  let component: DeleteRawMaterialComponent;
  let fixture: ComponentFixture<DeleteRawMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteRawMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteRawMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
