import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminOptions } from './admin-options';

describe('AdminOptions', () => {
  let component: AdminOptions;
  let fixture: ComponentFixture<AdminOptions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminOptions]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminOptions);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
