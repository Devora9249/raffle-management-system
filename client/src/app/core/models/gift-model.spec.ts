import { TestBed } from '@angular/core/testing';
import { GiftModel } from './gift-model';

describe('GiftModel', () => {
  let service: GiftModel;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GiftModel);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
