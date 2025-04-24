import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FormTableComponent } from './user/user.component';

export const routes: Routes = [
    {path: 'home', component: HomeComponent,
        children: [
            {path: 'form', component: FormTableComponent},
            {path: '', redirectTo: '', pathMatch: 'full'}
        ]
    }
];
