import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGiftDialog } from './edit-gift-dialog';

describe('EditGiftDialog', () => {
  let component: EditGiftDialog;
  let fixture: ComponentFixture<EditGiftDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditGiftDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditGiftDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
