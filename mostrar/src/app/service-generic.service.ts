
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServiceGenericService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  private getHeaders(): HttpHeaders {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    const token = currentUser?.token;

    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }


  get<T>(endpoint: string) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
  }

  getById<T>(endpoint: string, id: number) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  post<T>(endpoint: string, data: any) {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, data);
  }

  put<T>(endpoint: string, data: any) {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}`, data);
  }

  delete<T>(endpoint: string, id: number | string, mode: 'fisico' | 'Logical' = 'fisico') {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}/${id}?mode=${mode}`, {
      headers: this.getHeaders()
    });
  }

}
