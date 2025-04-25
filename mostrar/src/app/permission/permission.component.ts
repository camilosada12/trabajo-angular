import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { permission } from '../Interfaces/permission';
import { ServiceGenericService } from '../service-generic.service';

@Component({
  selector: 'app-permission',
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './permission.component.html',
  styleUrl: './permission.component.css'
})
export class PermissionComponent {

  permissions: permission[] = [];
  currentPermission: permission = this.getEmptyPermission();
  showPermission: boolean = false;
  isEditing: boolean = false;

  constructor(private PermissionServices: ServiceGenericService) { }

  ngOnInit(): void {
    this.loadPermission();
  }

  // Helper para crear un formulario vacío
  getEmptyPermission(): permission {
    return {
      id: 0,
      name: '',
      description: '',
      active: true
    };
  }

  loadPermission(): void {
    this.PermissionServices.get<permission[]>('Permission').subscribe({
      next: data => this.permissions = data,
      error: err => console.error('Error al cargar los formularios', err)
    });
  }

  // Maneja tanto la creación como la actualización
  submitPermission(): void {
    if (this.isEditing) {
      this.updatePermission();
    } else {
      this.addPermission();
    }
  }

  addPermission(): void {
    this.PermissionServices.post<permission>('Permission', this.currentPermission).subscribe({
      next: module => {
        this.permissions.push(module);
        this.resetPermission();
      },
      error: err => console.error('Error al agregar formulario', err)
    });
  }

  editPermission(module: permission): void {
    this.isEditing = true;
    this.currentPermission = { ...module }; // Copia del objeto para no modificar directamente
    this.showPermission = true;           // Muestra el formulario
  }

  updatePermission(): void {
    this.PermissionServices.put<permission>('Permission', this.currentPermission).subscribe({
      next: () => {
        this.loadPermission();
        this.resetPermission();
      },
      error: err => console.error('Error al actualizar formulario', err)
    });
  }


  deletePermission(id: number): void {
    this.PermissionServices.delete<permission>('Permission', id).subscribe({
      next: () => this.permissions = this.permissions.filter(f => f.id !== id),
      error: err => console.error('Error al eliminar formulario', err)
    });
  }

  deletePermissionLogic(id: number): void {
    this.PermissionServices.deleteLogic<permission>('Permission', id).subscribe({
      next: () => this.permissions = this.permissions.filter(f => f.id !== id),
      error: err => console.error('Error al eliminar lógicamente', err)
    });
  }


  togglePermission(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentPermission = this.getEmptyPermission();
    }
    this.showPermission = !this.showPermission;
  }

  cancelPermission(): void {
    this.resetPermission();
  }

  resetPermission(): void {
    this.showPermission = false;
    this.isEditing = false;
    this.currentPermission = this.getEmptyPermission();
  }
}
