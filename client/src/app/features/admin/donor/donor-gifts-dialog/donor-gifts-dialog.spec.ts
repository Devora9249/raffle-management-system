import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorGiftsDialog } from './donor-gifts-dialog';

describe('DonorGiftsDialog', () => {
  let component: DonorGiftsDialog;
  let fixture: ComponentFixture<DonorGiftsDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorGiftsDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorGiftsDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
