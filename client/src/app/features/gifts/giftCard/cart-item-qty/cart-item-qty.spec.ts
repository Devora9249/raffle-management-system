import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartItemQty } from './cart-item-qty';

describe('CartItemQty', () => {
  let component: CartItemQty;
  let fixture: ComponentFixture<CartItemQty>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartItemQty]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartItemQty);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
