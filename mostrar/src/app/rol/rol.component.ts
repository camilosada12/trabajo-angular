import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { rol } from '../Interfaces/rol';
import { ServiceGenericService } from '../service-generic.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-rol',
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './rol.component.html',
  styleUrl: './rol.component.css'
})
export class RolComponent {
  rols: rol[] = [];
  currentRol: rol = this.getEmptyRol();
  showRol: boolean = false;
  isEditing: boolean = false;
  userRole : string | null = null;

  constructor(private rolServices: ServiceGenericService, private auth : AuthService) { }

  ngOnInit(): void {
    this.loadRol();
    this.userRole = this.auth.getUserRole();
  }

  // Helper para crear un formulario vacío
  getEmptyRol(): rol {
    return {
      id: 0,
      name: '',
      description: ''
    };
  }

  loadRol(): void {
    this.rolServices.get<rol[]>('Rol').subscribe({
      next: data => {
        console.log('Datos recibidos:', data);  // Añade esta línea
        this.rols = data;
      },
      error: err => console.error('Error al cargar los formularios', err)
    });
  }
  // Maneja tanto la creación como la actualización
  submitRol(): void {
    if (this.isEditing) {
      this.updateRol();
    } else {
      this.addRol();
    }
  }

  addRol(): void {

    this.rolServices.post<rol>('Rol', this.currentRol).subscribe({
      next: module => {
        this.rols.push(module);
        this.resetRol();
      },
      error: err => console.error('Error al agregar formulario', err)
    });
  }

  editRol(module: rol): void {
    this.isEditing = true;
    this.currentRol = { ...module }; // Copia del objeto para no modificar directamente
    this.showRol = true;           // Muestra el formulario
  }

  updateRol(): void {
    this.rolServices.put<rol>('Rol', this.currentRol).subscribe({
      next: () => {
        this.loadRol();
        this.resetRol();
      },
      error: err => console.error('Error al actualizar formulario', err)
    });
  }


  deleteRol(id: number,  mode: 'fisico' | 'Logical' = 'fisico'): void {
    this.rolServices.delete<rol>('Rol', id, mode).subscribe({
      next: () => this.rols = this.rols.filter(f => f.id !== id),
      error: err => console.error(`Error al eliminar (${mode}) formulario`, err)
    });
  }



  toggleRol(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentRol = this.getEmptyRol();
    }
    this.showRol = !this.showRol;
  }

  cancelRol(): void {
    this.resetRol();
  }

  resetRol(): void {
    this.showRol = false;
    this.isEditing = false;
    this.currentRol = this.getEmptyRol();
  }
}
