import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FuneralListComponent } from './funeral-list.component';

describe('FuneralListComponent', () => {
  let component: FuneralListComponent;
  let fixture: ComponentFixture<FuneralListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FuneralListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FuneralListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
