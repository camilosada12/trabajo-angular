import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'token';

  constructor(private route: Router) { }

  SetToken(token: string) {
    localStorage.setItem(this.tokenKey, token)
  }
    
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey)
  }

  cerrarSesion() {
    localStorage.removeItem(this.tokenKey);
    this.route.navigate(['/login'])
  }

  private validarTimeToken(token: string): boolean {
    try {
      const decode: any = jwtDecode(token)
      const expiracion = decode.exp;
      return Date.now() < expiracion * 1000;
    } catch {
      return false
    }
  }

  public validarCredenciales(): boolean {
    const token = this.getToken();
    if (!token || !this.validarTimeToken(token)) {
      return false;
    } else {
      return true
    }
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    } catch {
      return null;
    }
  }

  getEmailFromToken(): string | null {
  const token = this.getToken();
  if (!token) return null;

  try {
    const decoded: any = jwtDecode(token);
    return decoded.email || decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || null;
  } catch {
    return null;
  }
}

}
