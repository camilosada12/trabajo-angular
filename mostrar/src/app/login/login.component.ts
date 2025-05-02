import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms'; // 👈 Importar FormsModule  // El servicio que crearemos a continuación

@Component({
  selector: 'app-login',
  imports : [FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData = { username: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  onLogin(): void {
    this.authService.login(this.loginData).subscribe({
      next: (response: any) => {
        this.router.navigate(['/home']);  // Redirigir a la página de inicio
      },
      error: (error) => {
        console.error('Error en el login', error);
        alert('Credenciales incorrectas, por favor intente nuevamente.');
      }
    });
  }
}