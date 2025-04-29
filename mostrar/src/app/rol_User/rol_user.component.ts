import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { ServiceGenericService } from '../service-generic.service';
import { Rol_User } from '../Interfaces/Rol_User';

@Component({
  selector: 'app-rol-user-table',
  standalone: true,
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './rol_user.component.html',
  styleUrls: ['./rol_user.component.css']
})
export class RolUserTableComponent implements OnInit {

  rolUsers: Rol_User[] = [];
  currentRolUser: Rol_User = this.getEmptyRolUser();
  showRolUser: boolean = false;
  isEditing: boolean = false;
  users: { id: number, name: string }[] = [];
  roles: { id: number, name: string }[] = [];

  constructor(private serviceGeneric: ServiceGenericService) { }

  ngOnInit(): void {
    this.loadRolUsers();
    this.loadUsers();
    this.loadRoles();
  }

  getEmptyRolUser(): Rol_User {
    return {
      id: 0,
      userId: 0,
      rolId: 0,
    };
  }

  loadUsers(): void {
    this.serviceGeneric.get<{ id: number, userName: string }[]>('User').subscribe({
      next: data => {
        this.users = data.map(u => ({
          id: u.id,
          name: u.userName
        }));
      },
      error: err => console.error('error al cargar usuarios', err)
    });
  }

  loadRoles(): void {
    this.serviceGeneric.get<{ id: number, name: string }[]>('Rol').subscribe({
      next: data => {
        this.roles = data.map(r => ({
          id: r.id,
          name: r.name
        }));
      },
      error: err => console.error('error al cargar roles', err)
    });
  }

  loadRolUsers(): void {
    this.serviceGeneric.get<Rol_User[]>('RolUser').subscribe({
      next: data => {
        this.rolUsers = data;
      },
      error: err => console.error('error al cargar rol_users', err)
    });
  }

  submitRolUser(): void {
    const existingRolUser = this.rolUsers.find(ru => 
      ru.userId === this.currentRolUser.userId
    );
  
    if (existingRolUser && !this.isEditing) {
      // si ya existe y no estamos editando, simplemente actualizamos el rol
      this.currentRolUser.id = existingRolUser.id; // usamos el id existente
      this.isEditing = true;
      this.updateRolUser();
    } else if (this.isEditing) {
      this.updateRolUser();
    } else {
      this.addRolUser();
    }
  }
  

  addRolUser(): void {
    const payload = {
      userId: this.currentRolUser.userId,
      rolId: this.currentRolUser.rolId
    };
  
    this.serviceGeneric.post<Rol_User>('RolUser', payload).subscribe({
      next: rolUser => {
        this.rolUsers.push(rolUser);
        this.resetRolUser();
      },
      error: err => {
        console.error('error al agregar rol_user', err);
      }
    });
  }
  

  editRolUser(rolUser: Rol_User): void {
    this.currentRolUser = {
      ...rolUser, // copiamos todo el objeto rolUser (id, userId, rolId, etc)
    };
    this.isEditing = true;
    this.showRolUser = true;
  }
  

  updateRolUser(): void {
    this.serviceGeneric.put<Rol_User>('RolUser', this.currentRolUser).subscribe({
      next: () => {
        this.loadRolUsers();
        this.resetRolUser();
      },
      error: err => console.error('error al actualizar rol_user', err)
    });
  }

  deleteRolUser(id: number): void {
    this.serviceGeneric.delete<Rol_User>('RolUser', id).subscribe({
      next: () => this.rolUsers = this.rolUsers.filter(ru => ru.id !== id),
      error: err => console.error('error al eliminar rol_user', err)
    });
  }

  deleteRolUserLogic(id: number): void {
    this.serviceGeneric.deleteLogic<Rol_User>('RolUser', id).subscribe({
      next: () => {
        const rolUser = this.rolUsers.find(ru => ru.id === id);
        if (rolUser) {
          rolUser.isdeleted = true;
        }
      },
      error: err => console.error('error al eliminar lógicamente rol_user', err)
    });
  }

  toggleRolUser(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentRolUser = this.getEmptyRolUser();
    }
    this.showRolUser = !this.showRolUser;
  }

  cancelRolUser(): void {
    this.resetRolUser();
  }

  resetRolUser(): void {
    this.showRolUser = false;
    this.isEditing = false;
    this.currentRolUser = this.getEmptyRolUser();
  }

  // 🔽 funciones para mostrar nombres en el template
  getUserName(userId: number): string {
    const user = this.users.find(u => u.id === userId);
    return user ? user.name : 'desconocido';
  }

  getRolName(rolId: number): string {
    const rol = this.roles.find(r => r.id === rolId);
    return rol ? rol.name : 'desconocido';
  }

  get availableUsers() {
  return this.users.filter(user => 
    !this.rolUsers.some(rolUser => rolUser.userId === user.id) || 
    user.id === this.currentRolUser.userId
  );
}

  
}
