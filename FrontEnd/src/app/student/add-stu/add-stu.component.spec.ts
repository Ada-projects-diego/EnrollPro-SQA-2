import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStuComponent } from './add-stu.component';

describe('AddStuComponent', () => {
  let component: AddStuComponent;
  let fixture: ComponentFixture<AddStuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddStuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddStuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
