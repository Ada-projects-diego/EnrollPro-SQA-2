import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCouComponent } from './add-cou.component';

describe('AddCouComponent', () => {
  let component: AddCouComponent;
  let fixture: ComponentFixture<AddCouComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCouComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCouComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
