import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WinnigsPage } from './winnigs-page';

describe('WinnigsPage', () => {
  let component: WinnigsPage;
  let fixture: ComponentFixture<WinnigsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WinnigsPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WinnigsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
