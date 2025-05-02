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
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },

  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard], // protege el acceso
    children: [
      { path: '', redirectTo: 'form', pathMatch: 'full' },
      { path: 'form', component: FormTableComponent },
      { path: 'module', component: ModuleComponent },
      { path: 'person', component: PersonComponent },
      { path: 'permission', component: PermissionComponent },
      { path: 'rol', component: RolComponent },
      { path: 'user', component: UserTableComponent },
      { path: 'rolUser', component: RolUserTableComponent },
      { path: 'formModule', component: FormModuleComponent },
    ]
  },
  { path: '**', redirectTo: 'login' } // para rutas no válidas
];