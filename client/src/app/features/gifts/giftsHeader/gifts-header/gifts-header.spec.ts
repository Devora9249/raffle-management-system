import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftsHeader } from './gifts-header';

describe('GiftsHeader', () => {
  let component: GiftsHeader;
  let fixture: ComponentFixture<GiftsHeader>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftsHeader]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftsHeader);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
