import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(): boolean {
    const currentUser = this.authService.currentUserValue;
    if (currentUser && currentUser.token) {
      return true;  // Usuario autenticado, puede acceder a la ruta
    }
    this.router.navigate(['/login']);  // Redirigir al login si no está autenticado
    return false;
  }
}
