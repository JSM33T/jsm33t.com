import { TestBed } from '@angular/core/testing';

import { HttpapiService } from './httpapi-service';

describe('HttpapiService', () => {
  let service: HttpapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
