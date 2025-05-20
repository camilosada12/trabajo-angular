import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GoogleServicesService {
  private URL = 'https://localhost:7205/api/Email/enviar-correo';

  constructor(private http: HttpClient) { }

  enviarCorreo(email: string): Observable<any> {
    return this.http.post(this.URL, { email }, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
}
