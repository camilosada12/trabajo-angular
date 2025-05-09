import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FormTableComponent } from './form/form.component';
import { ModuleComponent } from './module/module.component';
import { PersonComponent } from './person/person.component';
import { PermissionComponent } from './permission/permission.component';
import { RolComponent } from './rol/rol.component';
import { UserTableComponent } from './user/user.component';
import { RolUserTableComponent } from './rol_User/rol_user.component';
import { FormModuleComponent } from './form-module/form-module.component';
import { LoginComponent } from './login/login.component';
import { authGuard } from './guards/auth.guard';


export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },

  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'form', pathMatch: 'full' },
      { path: 'form', component: FormTableComponent , canActivate: [authGuard]},
      { path: 'module', component: ModuleComponent, canActivate: [authGuard]},
      { path: 'person', component: PersonComponent ,  canActivate: [authGuard]},
      { path: 'permission', component: PermissionComponent, canActivate: [authGuard] },
      { path: 'rol', component: RolComponent , canActivate: [authGuard]},
      { path: 'user', component: UserTableComponent , canActivate: [authGuard] },
      { path: 'rolUser', component: RolUserTableComponent , canActivate: [authGuard]},
      { path: 'formModule', component: FormModuleComponent , canActivate: [authGuard]},
    ]
  },
  { path: '**', redirectTo: 'login' } // para rutas no válidas
];