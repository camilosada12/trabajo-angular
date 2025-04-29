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
export class UserTableComponent implements OnInit {

  users: User[] = [];
  currentUser: User = this.getEmptyUser();
  showUser: boolean = false;
  isEditing: boolean = false;
  persons: { id: number, name: string }[] = [];


  constructor(private UserServices: ServiceGenericService) { }

  ngOnInit(): void {
    this.loadUser();
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

  getPersonName(personId: number): string {
    const person = this.persons.find(p => p.id === personId);
    return person ? person.name : 'desconocido';
  }
  
  // Carga todas las personas sin filtrar para mostrar en la tabla
  loadAllPersons(): void {
    this.UserServices.get<{ id: number, firstName: string, lastName: string }[]>('Person').subscribe({
      next: data => {
        this.persons = data.map(p => ({
          id: p.id,
          name: `${p.firstName} ${p.lastName}`
        }));
      },
      error: err => console.error('error al cargar personas', err)
    });
  }

  // Carga personas disponibles para el formulario
  loadAvailablePersons(currentPersonId?: number): void {
    this.UserServices.get<{ id: number, firstName: string, lastName: string }[]>('Person').subscribe({
      next: data => {
        const usedPersonIds = this.users.map(u => u.personId);
  
        this.persons = data
          .filter(p => !usedPersonIds.includes(p.id) || p.id === currentPersonId)
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
        this.loadAllPersons(); // Cargar todas las personas para mostrar en la tabla
      },
      error: err => console.error('Error al cargar los usuarios', err)
    });
  }
  
  // Maneja tanto la creación como la actualización
  submitUser(): void {
    const duplicateUsername = this.users.find(u => u.userName === this.currentUser.userName && u.id !== this.currentUser.id);
    const duplicateEmail = this.users.find(u => u.email === this.currentUser.email && u.id !== this.currentUser.id);
    const duplicatePassword = this.users.find(u => u.password === this.currentUser.password && u.id !== this.currentUser.id);
    const duplicatePerson = this.users.find(u => u.personId === this.currentUser.personId && u.id !== this.currentUser.id);
  
    if (duplicateUsername) {
      alert('ya existe un usuario con este username.');
      return;
    }
  
    if (duplicateEmail) {
      alert('ya existe un usuario con este email.');
      return;
    }
  
    if (duplicatePassword) {
      alert('ya existe un usuario con esta contraseña.');
      return;
    }
  
    if (duplicatePerson) {
      alert('ya existe un usuario para esta persona.');
      return;
    }
  
    if (this.isEditing) {
      this.updateUser();
    } else {
      this.addUser();
    }
  }
  
  addUser(): void {
    this.UserServices.post<User>('User', this.currentUser).subscribe({
      next: user => {
        this.users.push(user);
        this.resetUser();
        this.loadUser(); // Recargar todo para mantener la consistencia
      },
      error: err => console.error('error al agregar usuario', err)
    });
  }
  
  editPerson(user: User): void {
    this.isEditing = true;
    this.currentUser = { ...user }; // Copia del objeto
    this.showUser = true;
  
    this.loadAvailablePersons(user.personId); // Cargar personas disponibles incluyendo la actual
  }
  
  updateUser(): void {
    this.UserServices.put<User>('User', this.currentUser).subscribe({
      next: () => {
        this.loadUser(); // Recargar todo para mantener la consistencia
        this.resetUser();
      },
      error: err => console.error('error al actualizar usuario', err)
    });
  }

  deleteUser(id: number): void {
    this.UserServices.delete<User>('User', id).subscribe({
      next: () => this.users = this.users.filter(f => f.id !== id),
      error: err => console.error('Error al eliminar usuario', err)
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
      this.loadAvailablePersons(); // Cargar solo personas disponibles para el formulario
    }
    this.showUser = !this.showUser;
  }
  
  cancelPerson(): void {
    this.resetUser();
    this.loadAllPersons(); // Recargar todas las personas al cancelar
  }
  
  resetUser(): void {
    this.showUser = false;
    this.isEditing = false;
    this.currentUser = this.getEmptyUser();
    this.loadAllPersons(); // Recargar todas las personas al resetear
  }
}