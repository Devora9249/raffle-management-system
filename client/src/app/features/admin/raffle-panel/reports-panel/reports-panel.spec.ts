import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportsPanel } from './reports-panel';

describe('ReportsPanel', () => {
  let component: ReportsPanel;
  let fixture: ComponentFixture<ReportsPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReportsPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReportsPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
