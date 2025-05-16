import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import Swal from 'sweetalert2';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.validarCredenciales()) {
    return true;
  } else {
    Swal.fire({
      icon: 'error',
      title: 'Acceso denegado',
      text: 'No tienes permisos para acceder a esta página. Por favor, inicia sesión.',
      confirmButtonText: 'OK'
    }).then(() => {
      router.navigate(['/']);
    });
    
    return false;
  }
};
