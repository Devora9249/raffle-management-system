import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchasePanel } from './purchase-panel';

describe('PurchasePanel', () => {
  let component: PurchasePanel;
  let fixture: ComponentFixture<PurchasePanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchasePanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchasePanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
