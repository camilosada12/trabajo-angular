import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  private apiUrl = 'https://localhost:7205/api/Login/google'; // <-- login con minúscula, debe coincidir con tu backend

  constructor(private http: HttpClient) { }

  // función para enviar el token de google al backend
  googleLogin(googleTokenDto: { token: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, googleTokenDto);
  }
}
