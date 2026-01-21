import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorPanel } from './donor-panel';

describe('DonorPanel', () => {
  let component: DonorPanel;
  let fixture: ComponentFixture<DonorPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
