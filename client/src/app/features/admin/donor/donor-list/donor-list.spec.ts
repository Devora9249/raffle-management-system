import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorList } from './donor-list';

describe('DonorList', () => {
  let component: DonorList;
  let fixture: ComponentFixture<DonorList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
