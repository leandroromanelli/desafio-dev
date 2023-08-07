import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CnabModel } from '../models/cnab';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CnabService {

  constructor(private http: HttpClient) { }

  uploadCnab(file: File): Observable<CnabModel[]> {
    const formData = new FormData();
    formData.append("file", file, file.name);

    return this.http.post<any>('http://localhost:5000/api/cnab', formData)
  }

}
