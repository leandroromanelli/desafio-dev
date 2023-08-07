import { HttpErrorResponse } from '@angular/common/http';
import { CnabService } from './services/cnab.service';
import { Component } from '@angular/core';
import { CnabModel } from './models/cnab';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: string = 'frontend';
  file: File | null = null;
  hasData: boolean = false;
  message: string = '';
  cnabList: CnabModel[] = [];
  formatter: Intl.NumberFormat = new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  });

  constructor(private cnabService: CnabService){}

  upload(event : any): void {
    if (event.target.files.length == 0) {
      return;
    }

    this.file = event.target.files[0];
    this.hasData = true;
    this.message = '';
    this.cnabList = [];
  }

  send(): void{
    if(this.file === null)
      return;

    this.cnabService.uploadCnab(this.file).subscribe(
    (result) => {
      this.file = null;
      this.hasData = false;
      this.cnabList = result;
    },
    (err: HttpErrorResponse) => {
      this.file = null;
      this.message = err.statusText;
    });
  }

  localeDate(date: string) : string {
    const convertedString = (new Date(date));
    return `${convertedString.toLocaleDateString('pt-BR')} ${convertedString.toLocaleTimeString('pt-BR')}`;
  }

  localeCurrency(value: number) : string {
    return this.formatter.format(value);
  }

  localeDocument(document: string): string {
    return `${document.substr(0, 3)}.${document.substr(3, 3)}.${document.substr(6, 3)}-${document.substr(9, 2)}`;
  }

  localeCard(card: string): string {
    return `${card.substr(0,4)}-${card.substr(4,2)}-${card.substr(6,2)}-${card.substr(8,4)}`;
  }
}
