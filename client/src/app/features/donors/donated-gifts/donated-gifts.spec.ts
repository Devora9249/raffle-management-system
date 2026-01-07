import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonatedGifts } from './donated-gifts';

describe('DonatedGifts', () => {
  let component: DonatedGifts;
  let fixture: ComponentFixture<DonatedGifts>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonatedGifts]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonatedGifts);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
