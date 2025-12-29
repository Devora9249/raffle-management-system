import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SortPanel } from './sort-panel';

describe('SortPanel', () => {
  let component: SortPanel;
  let fixture: ComponentFixture<SortPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SortPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SortPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
