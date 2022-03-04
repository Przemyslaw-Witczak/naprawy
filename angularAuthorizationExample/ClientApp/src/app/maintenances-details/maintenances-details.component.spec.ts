import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenancesDetailsComponent } from './maintenances-details.component';

describe('MaintenancesDetailsComponent', () => {
  let component: MaintenancesDetailsComponent;
  let fixture: ComponentFixture<MaintenancesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MaintenancesDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintenancesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
