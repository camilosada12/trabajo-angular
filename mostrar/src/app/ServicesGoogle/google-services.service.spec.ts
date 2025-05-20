import { TestBed } from '@angular/core/testing';

import { GoogleServicesService } from './google-services.service';

describe('GoogleServicesService', () => {
  let service: GoogleServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GoogleServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
