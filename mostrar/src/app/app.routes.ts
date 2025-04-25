import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FormTableComponent } from './form/form.component';
import { ModuleComponent } from './module/module.component';
import { PersonComponent } from './person/person.component';


export const routes: Routes = [
    {path: 'home', component: HomeComponent,
        children: [
            {path: 'form', component: FormTableComponent},
            {path: '', redirectTo: '', pathMatch: 'full'},
            {path: 'module', component: ModuleComponent},
            {path: '', redirectTo: '', pathMatch: 'full'},
            {path: 'person', component: PersonComponent},
            {path: '', redirectTo: '', pathMatch: 'full'}
        ]
    }
];
