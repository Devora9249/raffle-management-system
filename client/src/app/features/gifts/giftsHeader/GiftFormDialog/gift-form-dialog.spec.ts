import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftFormDialog } from './gift-form-dialog';

describe('GiftFormDialog', () => {
  let component: GiftFormDialog;
  let fixture: ComponentFixture<GiftFormDialog>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftFormDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftFormDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
