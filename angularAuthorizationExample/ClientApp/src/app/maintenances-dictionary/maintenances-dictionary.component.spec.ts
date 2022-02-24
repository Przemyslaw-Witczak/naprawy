import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenancesDictionaryComponent } from './maintenances-dictionary.component';

describe('MaintenancesDictionaryComponent', () => {
  let component: MaintenancesDictionaryComponent;
  let fixture: ComponentFixture<MaintenancesDictionaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MaintenancesDictionaryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintenancesDictionaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
