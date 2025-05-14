import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../ServicesLogin/services.service';

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

  constructor(private authService: ServicesService) { }

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

    // Configurar Google Identity Services
    window.google.accounts.id.initialize({
      client_id: '945077191720-barmlufl5g1njje5rd58bko4pvfjungv.apps.googleusercontent.com',
      callback: this.handleCredentialResponse.bind(this),
      auto_select: false
    });
    
    // Opcional: Renderizar el botón de Google (si prefieres usar el botón personalizado de Google)
    // window.google.accounts.id.renderButton(
    //   document.getElementById('googleSignInButton'),
    //   { theme: 'outline', size: 'large' }
    // );

    console.log('Google Identity Services inicializado');
  }

  // Función de callback para manejar la respuesta de Google
  private handleCredentialResponse(response: any) {
    console.log('token recibido:', response.credential);
    
    // Enviar el token al servicio para autenticar
    this.authService.googleLogin(response.credential).subscribe({
      next: (response) => {
        console.log('login exitoso', response);
        localStorage.setItem('jwt_token', response.token);
      },
      error: (err) => {
        console.error('error en login google', err);
      }
    });
  }

  // Login con Google - debe ser llamado desde un botón o evento de usuario
  onGoogleSignIn() {
    if (!window.google || !window.google.accounts) {
      console.error('Google Identity Services no inicializado');
      return;
    }

    // Muestra el prompt de selección de cuenta de Google
    window.google.accounts.id.prompt();
  }
}