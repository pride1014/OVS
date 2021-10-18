import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookShiftComponent } from './book-shift.component';

describe('BookShiftComponent', () => {
  let component: BookShiftComponent;
  let fixture: ComponentFixture<BookShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookShiftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
