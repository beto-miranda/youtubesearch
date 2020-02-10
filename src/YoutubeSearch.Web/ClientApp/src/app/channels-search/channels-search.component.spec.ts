import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChannelsSearchComponent } from './channels-search.component';

describe('ChannelsSearchComponent', () => {
  let component: ChannelsSearchComponent;
  let fixture: ComponentFixture<ChannelsSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChannelsSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChannelsSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
