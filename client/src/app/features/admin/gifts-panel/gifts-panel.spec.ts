import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftsPanel } from './gifts-panel';

describe('GiftsPanel', () => {
  let component: GiftsPanel;
  let fixture: ComponentFixture<GiftsPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftsPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftsPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
