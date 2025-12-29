import { TestBed } from '@angular/core/testing';

import { GiftDto } from './gift-dto';

describe('GiftDto', () => {
  let service: GiftDto;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GiftDto);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
