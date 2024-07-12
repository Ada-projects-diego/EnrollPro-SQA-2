import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayCouComponent } from './display-cou.component';

describe('DisplayCouComponent', () => {
  let component: DisplayCouComponent;
  let fixture: ComponentFixture<DisplayCouComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayCouComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisplayCouComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
