import { HttpErrorResponse } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { AppComponent } from './app.component';
import { CnabService } from './services/cnab.service';
import { CnabModel } from './models/cnab';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let mockCnabService: jasmine.SpyObj<CnabService>;

  beforeEach(async () => {
    mockCnabService = jasmine.createSpyObj('CnabService', ['uploadCnab']);

    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      providers: [{ provide: CnabService, useValue: mockCnabService }],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should upload a file and update cnabList', () => {
    const file = new File([''], 'test.txt');
    const mockCnabList: CnabModel[] = [];

    mockCnabService.uploadCnab.and.returnValue(of(mockCnabList));

    component.upload({ target: { files: [file] } });
    component.send();

    expect(component.cnabList).toEqual(mockCnabList);
    expect(component.file).toBeNull();
    expect(component.hasData).toBeFalsy();
    expect(component.message).toEqual('');
  });

  it('should handle upload error', () => {
    const mockError = new HttpErrorResponse({
      status: 404,
      statusText: 'Not Found',
    });

    mockCnabService.uploadCnab.and.returnValue(throwError(mockError));

    component.upload({ target: { files: [new File([''], 'test.txt')] } });
    component.send();

    expect(component.cnabList).toEqual([]);
    expect(component.file).toBeNull();
    expect(component.hasData).toBeTrue();
    expect(component.message).toEqual('Not Found');
  });

  it('should format date in locale', () => {
    const date = '2023-08-07T12:34:56';

    expect(component.localeDate(date)).toContain('07/08/2023');
  });

  it('should format currency in locale', () => {
    const value = 123456.78;

    expect(component.localeCurrency(value)).toContain('R$');
    expect(component.localeCurrency(value)).toContain('123.456,78');
  });

  it('should format document in locale', () => {
    const document = '12345678901';

    expect(component.localeDocument(document)).toEqual('123.456.789-01');
  });

  it('should format card in locale', () => {
    const card = '1234567890123456';

    expect(component.localeCard(card)).toEqual('1234-56-78-9012');
  });
});
