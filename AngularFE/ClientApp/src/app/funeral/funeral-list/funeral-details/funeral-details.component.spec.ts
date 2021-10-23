import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FuneralDetailsComponent } from './funeral-details.component';

describe('FuneralDetailsComponent', () => {
  let component: FuneralDetailsComponent;
  let fixture: ComponentFixture<FuneralDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FuneralDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FuneralDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
