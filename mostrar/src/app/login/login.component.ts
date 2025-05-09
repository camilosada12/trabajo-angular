import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { Route, Router, Routes } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { GeneralService } from '../enpoint/general.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private ApiService: GeneralService, private router: Router, private auth: AuthService) {
    this.loginForm = fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login() {
    if (this.loginForm.valid) {
      const credenciales = this.loginForm.value;

      this.ApiService.login(credenciales).subscribe({
        next: (data) => {
          this.auth.SetToken(data.token); // guarda el token (asegúrate que lo hace correctamente)
          this.router.navigate(['/home']); // redirige a /user
        },
        error: (err) => {
          console.error('error al iniciar sesión', err);
          // puedes mostrar un mensaje de error aquí si quieres
        }
      });
    }

  }
}
