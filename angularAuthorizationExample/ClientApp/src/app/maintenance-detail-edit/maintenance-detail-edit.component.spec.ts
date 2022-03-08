import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceDetailEditComponent } from './maintenance-detail-edit.component';

describe('MaintenanceDetailEditComponent', () => {
  let component: MaintenanceDetailEditComponent;
  let fixture: ComponentFixture<MaintenanceDetailEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MaintenanceDetailEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintenanceDetailEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
