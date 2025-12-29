import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftsGrid } from './gifts-grid';

describe('GiftsGrid', () => {
  let component: GiftsGrid;
  let fixture: ComponentFixture<GiftsGrid>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftsGrid]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftsGrid);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
