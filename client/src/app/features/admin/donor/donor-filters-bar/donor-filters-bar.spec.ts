import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorFiltersBar } from './donor-filters-bar';

describe('DonorFiltersBar', () => {
  let component: DonorFiltersBar;
  let fixture: ComponentFixture<DonorFiltersBar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorFiltersBar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorFiltersBar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
