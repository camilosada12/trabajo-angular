import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { ServiceGenericService } from '../service-generic.service';
import { Form } from '../Interfaces/form';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-form-table',
  standalone: true,
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})

export class FormTableComponent implements OnInit {
  allForms: Form[] = []; // Array único para todos los formularios
  currentForm: Form = this.getEmptyForm();
  showForm: boolean = false;
  isEditing: boolean = false;
  userRole: string | null = null;

  constructor(private formService: ServiceGenericService, private auth: AuthService) { }

  ngOnInit(): void {
    this.userRole = this.auth.getUserRole();
    console.log('ROL:', this.userRole);
    this.loadForms();
  }

  getEmptyForm(): Form {
    return {
      id: 0,
      name: '',
      description: '',
      active: true,
      isDeleted: false
    };
  }

  loadForms(): void {
    if (this.userRole === 'Administrador') {
      // Admin ve todos los formularios (activos e inactivos)
      this.formService.get<Form[]>('FormControllerPrueba/deleted').subscribe({
        next: data => {
          this.allForms = data; // Mostrar todos sin filtrar
        },
        error: err => console.error('Error al cargar todos los formularios', err)
      });
    } else {
      // Usuario normal solo ve formularios activos
      this.formService.get<Form[]>('FormControllerPrueba').subscribe({
        next: data => {
          this.allForms = data.filter(f => f.active); // Solo activos
        },
        error: err => console.error('Error al cargar formularios', err)
      });
    }
  }

  submitForm(): void {
    if (this.isEditing) {
      this.updateForm();
    } else {
      this.addForm();
    }
  }

  addForm(): void {
    this.formService.post<Form>('FormControllerPrueba', this.currentForm).subscribe({
      next: form => {
        this.allForms.push(form);
        this.resetForm();
      },
      error: err => console.error('Error al agregar formulario', err)
    });
  }

  editForm(form: Form): void {
    // Solo permitir editar formularios activos
    if (!form.active) {
      console.warn('No se pueden editar formularios eliminados');
      return;
    }
    
    this.isEditing = true;
    this.currentForm = { ...form };
    this.showForm = true;
  }

  updateForm(): void {
    this.formService.put<Form>('FormControllerPrueba', this.currentForm).subscribe({
      next: () => {
        this.loadForms();
        this.resetForm();
      },
      error: err => console.error('Error al actualizar formulario', err)
    });
  }

  deleteForm(id: number, mode: 'fisico' | 'Logical' = 'fisico'): void {
    this.formService.delete<Form>('FormControllerPrueba', id, mode).subscribe({
      next: () => {
        this.loadForms(); // Recargar la tabla
      },
      error: err => console.error(`Error al eliminar (${mode}) formulario`, err)
    });
  }

  reactivateForm(id: number): void {
  this.formService.patch<Form>('FormControllerPrueba', id).subscribe({
    next: (response) => {
      console.log('Formulario reactivado exitosamente:', response);
      this.loadForms(); // Recargar la tabla
    },
    error: err => {
      console.error('Error al reactivar formulario:', err);
      // Opcional: mostrar mensaje de error al usuario
      // this.showErrorMessage('No se pudo reactivar el formulario');
    }
  });
}

  toggleForm(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentForm = this.getEmptyForm();
    }
    this.showForm = !this.showForm;
  }

  cancelForm(): void {
    this.resetForm();
  }

  resetForm(): void {
    this.showForm = false;
    this.isEditing = false;
    this.currentForm = this.getEmptyForm();
  }
}