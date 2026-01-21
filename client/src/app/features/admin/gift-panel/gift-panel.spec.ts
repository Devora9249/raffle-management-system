import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftPanel } from './gift-panel';

describe('GiftPanel', () => {
  let component: GiftPanel;
  let fixture: ComponentFixture<GiftPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
