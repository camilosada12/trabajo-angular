import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../ServicesLogin/services.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

  declare global {
    interface Window {
      google: any;
    }
  }

@Component({
  selector: 'app-google-login-component',
  templateUrl: './google-login-component.component.html',
  styleUrls: ['./google-login-component.component.css']
})
export class GoogleLoginComponentComponent implements OnInit {

  constructor(private authService: ServicesService, private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.loadGoogleScript()
      .then(() => {
        this.initGoogleSignIn();
      })
      .catch((err) => {
        console.error('error al cargar google sdk', err);
      });
  }

  // cargar el script de google de forma segura
  loadGoogleScript(): Promise<void> {
    return new Promise((resolve, reject) => {
      if (window.google?.accounts?.id) {
        resolve();
        return;
      }

      const script = document.createElement('script');
      script.src = 'https://accounts.google.com/gsi/client';
      script.async = true;
      script.defer = true;
      script.onload = () => {
        console.log('sdk de google cargado');
        resolve();
      };
      script.onerror = () => reject('error cargando sdk de google');

      document.body.appendChild(script);
    });
  }

  // Inicializar Google Identity Services
  initGoogleSignIn() {
    if (!window.google || !window.google.accounts) {
      console.error('Google Accounts no está disponible');
      return;
    }

    // Configuración ampliada para Google Identity Services
    window.google.accounts.id.initialize({
      client_id: '945077191720-barmlufl5g1njje5rd58bko4pvfjungv.apps.googleusercontent.com',
      callback: this.handleCredentialResponse.bind(this),
      auto_select: false,
      cancel_on_tap_outside: true,
      context: 'signin',
      ux_mode: 'popup',
      itp_support: true
    });

    // Renderizar el botón de Google oficial
    setTimeout(() => {
      window.google.accounts.id.renderButton(
        document.getElementById('googleSignInButton'),
        {
          theme: 'outline',
          size: 'large',
          type: 'standard',
          shape: 'rectangular',
          text: 'signin_with',
          logo_alignment: 'left'
        }
      );
    }, 100);

    console.log('Google Identity Services inicializado');
  }

  // Función de callback para manejar la respuesta de Google
  private handleCredentialResponse(response: any) {

    // Validar que el token sea un JWT
    if (!response.credential || response.credential.split('.').length !== 3) {
      console.error('El token recibido no es válido:', response.credential);
      return;
    }
    

    // Enviar el token al backend
    this.authService.googleLogin({ token: response.credential }).subscribe({
      next: (data) => {

        if (data.isSucces) {
          this.auth.SetToken(data.token); // guarda el token
          this.router.navigate(['/home']); // redirige a /home
        } else {
          console.warn('usuario no registrado:', data.message);
          alert('el usuario no está registrado, por favor crea una cuenta primero.');
        }
      },
      error: (err) => {
        console.error('Error en login con Google', err);

        if (err.status === 404) {
          alert('el usuario no está registrado, por favor crea una cuenta primero.');
        } else {
          alert('ocurrió un error al iniciar sesión con google.');
        }
      }
    });


  }



  // Login con Google - debe ser llamado desde un botón o evento de usuario
  onGoogleSignIn() {
    if (!window.google || !window.google.accounts) {
      console.error('Google Identity Services no inicializado');
      return;
    }

    // Una alternativa para mostrar el prompt es:
    window.google.accounts.id.prompt((notification: any) => {
      if (notification.isNotDisplayed() || notification.isSkippedMoment()) {
        console.log('Google prompt no se mostró o fue omitido:', notification);
        // Puedes intentar con una solución alternativa aquí
      }
    });
  }
}