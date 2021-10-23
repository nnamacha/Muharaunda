import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FuneralEditComponent } from './funeral-edit.component';

describe('FuneralEditComponent', () => {
  let component: FuneralEditComponent;
  let fixture: ComponentFixture<FuneralEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FuneralEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FuneralEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
