import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditUserComponent } from './create-or-edit-user.component';

describe('CreateOrEditUserComponent', () => {
  let component: CreateOrEditUserComponent;
  let fixture: ComponentFixture<CreateOrEditUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateOrEditUserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateOrEditUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
