import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorFormDialog } from './donor-form-dialog';

describe('DonorFormDialog', () => {
  let component: DonorFormDialog;
  let fixture: ComponentFixture<DonorFormDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorFormDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorFormDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
