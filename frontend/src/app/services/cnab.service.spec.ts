import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { CnabService } from './cnab.service';

describe('CnabService', () => {
  let service: CnabService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CnabService],
    });
    service = TestBed.inject(CnabService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should upload a CNAB file', () => {
    const mockFile = new File([''], 'test.txt');
    const mockResponse: any[] = [{}];

    service.uploadCnab(mockFile).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpTestingController.expectOne(
      'http://localhost:5000/api/cnab'
    );
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });
});
