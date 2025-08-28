import { TestBed } from '@angular/core/testing';

import { Storeservice } from './storeservice';

describe('Storeservice', () => {
  let service: Storeservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Storeservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
