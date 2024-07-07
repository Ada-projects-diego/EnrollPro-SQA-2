import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayStuComponent } from './display-stu.component';

describe('DisplayStuComponent', () => {
  let component: DisplayStuComponent;
  let fixture: ComponentFixture<DisplayStuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayStuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisplayStuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
