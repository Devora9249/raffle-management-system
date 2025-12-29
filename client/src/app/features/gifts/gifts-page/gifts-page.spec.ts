import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftsPage } from './gifts-page';

describe('GiftsPage', () => {
  let component: GiftsPage;
  let fixture: ComponentFixture<GiftsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftsPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
