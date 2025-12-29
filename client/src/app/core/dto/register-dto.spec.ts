import { TestBed } from '@angular/core/testing';

import { RegisterDto } from './register-dto';

describe('RegisterDto', () => {
  let service: RegisterDto;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterDto);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
