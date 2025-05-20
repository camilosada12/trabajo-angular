import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

import { ServiceGenericService } from '../service-generic.service';
import { User } from '../Interfaces/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-user-table',
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
  usersList: { id: number, name: string }[] = [];
  userRole: string | null = null

  constructor(private userServices: ServiceGenericService, private auth: AuthService) { }

  ngOnInit(): void {
    this.loadUsers();
    this.userRole = this.auth.getUserRole();
  }

  getEmptyUser(): User {
    return {
      id: 0,
      userName: '',
      email: '',
      password: '',
      personId: 0
    };
  }

  getUserName(personId: number): string {
    const user = this.usersList.find(p => p.id === personId);
    return user ? user.name : 'desconocido';
  }

  // carga todos los usuarios sin filtrar para mostrar en la tabla
  loadAllUsersList(): void {
    this.userServices.get<{ id: number, firstName: string, lastName: string }[]>('Person').subscribe({
      next: data => {
        this.usersList = data.map(p => ({
          id: p.id,
          name: `${p.firstName} ${p.lastName}`
        }));
      },
      error: err => console.error('error al cargar usuarios', err)
    });
  }

  // carga usuarios disponibles para el formulario
  loadAvailableUsers(currentUserId?: number): void {
    this.userServices.get<{ id: number, firstName: string, lastName: string }[]>('Person').subscribe({
      next: data => {
        const usedUserIds = this.users.map(u => u.personId);

        this.usersList = data
          .filter(p => !usedUserIds.includes(p.id) || p.id === currentUserId)
          .map(p => ({
            id: p.id,
            name: `${p.firstName} ${p.lastName}`
          }));
      },
      error: err => console.error('error al cargar usuarios', err)
    });
  }

  loadUsers(): void {
    this.userServices.get<User[]>('User').subscribe({
      next: data => {
        console.log('datos recibidos:', data);
        this.users = data;
        this.loadAllUsersList(); // cargar todos los usuarios para mostrar en la tabla
      },
      error: err => console.error('error al cargar los usuarios', err)
    });
  }

  submitUser(): void {
    const duplicateUsername = this.users.find(u => u.userName === this.currentUser.userName && u.id !== this.currentUser.id);
    const duplicateEmail = this.users.find(u => u.email === this.currentUser.email && u.id !== this.currentUser.id);
    const duplicatePassword = this.users.find(u => u.password === this.currentUser.password && u.id !== this.currentUser.id);
    const duplicateUser = this.users.find(u => u.personId === this.currentUser.personId && u.id !== this.currentUser.id);

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

    if (duplicateUser) {
      alert('ya existe un usuario para este usuario.');
      return;
    }

    if (this.isEditing) {
      this.updateUser();
    } else {
      this.addUser();
    }
  }

  addUser(): void {
    // Clonamos currentUser sin el id
    const userToCreate = { ...this.currentUser };
    if (userToCreate.id === 0) {
      delete userToCreate.id; // elimina el campo id para que no se envíe
    }

    this.userServices.post<User>('User', userToCreate).subscribe({
      next: user => {
        this.users.push(user);
        this.resetUser();
        this.loadUsers(); // recargar todo para mantener la consistencia
      },
      error: err => console.error('error al agregar usuario', err)
    });
  }


  editUser(user: User): void {
    this.isEditing = true;
    this.currentUser = { ...user };
    this.showUser = true;

    this.loadAvailableUsers(user.personId); // cargar usuarios disponibles incluyendo el actual
  }

  updateUser(): void {
    this.userServices.put<User>('User', this.currentUser).subscribe({
      next: () => {
        this.loadUsers(); // recargar todo para mantener la consistencia
        this.resetUser();
      },
      error: err => console.error('error al actualizar usuario', err)
    });
  }

  deleteUser(id: number, mode: 'fisico' | 'Logical' = 'fisico'): void {
    this.userServices.delete<User>('User', id, mode).subscribe({
      next: () => this.users = this.users.filter(f => f.id !== id),
      error: err => console.error(`error al eliminar (${mode}) usuario`, err)
    });
  }



  toggleUser(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentUser = this.getEmptyUser();
      this.loadAvailableUsers(); // cargar solo usuarios disponibles para el formulario
    }
    this.showUser = !this.showUser;
  }

  cancelUser(): void {
    this.resetUser();
    this.loadAllUsersList(); // recargar todos los usuarios al cancelar
  }

  resetUser(): void {
    this.showUser = false;
    this.isEditing = false;
    this.currentUser = this.getEmptyUser();
    this.loadAllUsersList(); // recargar todos los usuarios al resetear
  }
}
