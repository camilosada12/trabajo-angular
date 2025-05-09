import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);

   const token = auth.getToken(); // puedes traerlo del localstorage o un servicio

   const clonedReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`, // puedes cambiar el nombre del header si necesitas
      'Content-Type': 'application/json', // opcional
    },
  });

  return next(clonedReq)
};
