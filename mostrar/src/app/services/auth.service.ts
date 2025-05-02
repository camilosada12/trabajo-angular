import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7205/api/Auth/login';  // Asegúrate de que esta URL apunte al backend correcto
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;
  private tokenExpirationTimer: any;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(localStorage.getItem('currentUser')!));
    this.currentUser = this.currentUserSubject.asObservable();

    // valida el token al recargar
    this.checkTokenExpiration();
  }

  login(credentials: { username: string, password: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, credentials).pipe(
      tap(response => {
        // Guardar tanto el token como los datos del usuario en localStorage
        localStorage.setItem('currentUser', JSON.stringify({
          user: response.person,
          token: response.token
        }));
        this.currentUserSubject.next({
          user: response.person,
          token: response.token
        });
      })
    );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  autoLogout(expirationDuration: number) {
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }

  private checkTokenExpiration() {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    if (currentUser?.token) {
      const decoded: any = jwtDecode(currentUser.token);
      const expirationDate = new Date(decoded.exp * 1000);

      if (expirationDate <= new Date()) {
        this.logout();
      } else {
        this.autoLogout(expirationDate.getTime() - new Date().getTime());
      }
    }
  }

  get currentUserValue() {
    return this.currentUserSubject.value;
  }
}
