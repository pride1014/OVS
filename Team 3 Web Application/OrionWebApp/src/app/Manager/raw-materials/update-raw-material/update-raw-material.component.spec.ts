import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRawMaterialComponent } from './update-raw-material.component';

describe('UpdateRawMaterialComponent', () => {
  let component: UpdateRawMaterialComponent;
  let fixture: ComponentFixture<UpdateRawMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateRawMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRawMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
