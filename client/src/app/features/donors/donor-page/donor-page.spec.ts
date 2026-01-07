import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorPage } from './donor-page';

describe('DonorPage', () => {
  let component: DonorPage;
  let fixture: ComponentFixture<DonorPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
