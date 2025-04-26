import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { User } from '../Interfaces/user';
import { ServiceGenericService } from '../service-generic.service';

@Component({
  selector: 'app-form-table',
  standalone: true,
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserTableComponent {

  users: User[] = [];
  currentUser: User = this.getEmptyUser();
  showUser: boolean = false;
  isEditing: boolean = false;
  persons: { id: number, name: string }[] = [];


  constructor(private UserServices: ServiceGenericService) { }

  ngOnInit(): void {
    this.loadUser();
    this.loadPersons();
  }

  // Helper para crear un formulario vacío
  getEmptyUser(): User {
    return {
      id: 0,
      userName: '',
      email: '',
      password: '',
      personId: 0
    };
  }

  loadPersons(): void {
    this.UserServices.get<{ id: number, firstName: string, lastName: string }[]>('Person').subscribe({
      next: data => {
        const usedPersonIds = this.users.map(u => u.personId);
        this.persons = data
          .filter(p => !usedPersonIds.includes(p.id))
          .map(p => ({
            id: p.id,
            name: `${p.firstName} ${p.lastName}`
          }));
      },
      error: err => console.error('error al cargar personas', err)
    });
  }
  
  
  
  

  loadUser(): void {
    this.UserServices.get<User[]>('User').subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.users = data;
        this.loadPersons(); // aquí
      },
      error: err => console.error('Error al cargar los formularios', err)
    });
  }
  
  
  
  // Maneja tanto la creación como la actualización
  submitUser(): void {
    if (this.isEditing) {
      this.updateUser();
    } else {
      this.addUser();
    }
  }

  addUser(): void {
    const alreadyExists = this.users.some(u => u.personId === this.currentUser.personId);

    if (alreadyExists) {
      alert('Ya existe un usuario para esta persona.');
      return;
    }

    this.UserServices.post<User>('User', this.currentUser).subscribe({
      next: user => {
        this.users.push(user);
        this.resetUser();
      },
      error: err => console.error('Error al agregar formulario', err)
    });
  }


  editPerson(user: User): void {
    this.isEditing = true;
    this.currentUser = { ...user }; // Copia del objeto para no modificar directamente
    this.showUser = true;           // Muestra el formulario
  }

  updateUser(): void {

    this.UserServices.put<User>('User', this.currentUser).subscribe({
      next: () => {
        this.loadUser();
        this.resetUser();
      },
      error: err => console.error('Error al actualizar formulario', err)
    });
  }


  deleteUser(id: number): void {
    this.UserServices.delete<User>('User', id).subscribe({
      next: () => this.users = this.users.filter(f => f.id !== id),
      error: err => console.error('Error al eliminar formulario', err)
    });
  }

  deleteUserLogic(id: number): void {
    this.UserServices.deleteLogic<User>('User', id).subscribe({
      next: () => {
        const user = this.users.find(u => u.id === id);
        if (user) {
          user.isDeleted = true;  // Marcamos el usuario como eliminado
        }
      },
      error: err => console.error('Error al eliminar lógicamente', err)
    });
  }






  togglePerson(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentUser = this.getEmptyUser();
    }
    this.showUser = !this.showUser;
  }

  cancelPerson(): void {
    this.resetUser();
  }

  resetUser(): void {
    this.showUser = false;
    this.isEditing = false;
    this.currentUser = this.getEmptyUser();
  }

}
