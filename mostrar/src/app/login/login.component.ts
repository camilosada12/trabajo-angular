import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { GeneralService } from '../enpoint/general.service';
import { GoogleLoginComponentComponent } from '../google-login-component/google-login-component.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, GoogleLoginComponentComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;

  @ViewChild('googleLogin') googleLoginComponent!: GoogleLoginComponentComponent;

  constructor(private fb: FormBuilder, private ApiService: GeneralService, private router: Router, private auth: AuthService) {
    this.loginForm = fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.loginForm.valid) {
      const credenciales = this.loginForm.value;

      this.ApiService.login(credenciales).subscribe({
        next: (data) => {
          this.auth.SetToken(data.token); // guarda el token
          this.router.navigate(['/home']); // redirige a /home
        },
        error: (err) => {
          console.error('error al iniciar sesión', err);
          // puedes mostrar un mensaje de error aquí si quieres
        }
      });
    }
  }

  onGoogleSignInClick() {
    if (this.googleLoginComponent) {
      this.googleLoginComponent.onGoogleSignIn();
    } else {
      console.error('GoogleLoginComponent no está cargado');
    }
  }
}