import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CzesciDictionaryComponent } from './czesci-dictionary.component';

describe('CzesciDictionaryComponent', () => {
  let component: CzesciDictionaryComponent;
  let fixture: ComponentFixture<CzesciDictionaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CzesciDictionaryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CzesciDictionaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
