import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RafflePanel } from './raffle-panel';

describe('RafflePanel', () => {
  let component: RafflePanel;
  let fixture: ComponentFixture<RafflePanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RafflePanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RafflePanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
