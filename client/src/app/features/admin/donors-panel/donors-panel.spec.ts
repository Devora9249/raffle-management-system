import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorsPanel } from './donors-panel';

describe('DonorsPanel', () => {
  let component: DonorsPanel;
  let fixture: ComponentFixture<DonorsPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorsPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorsPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
