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
  forms: Form[] = [];
  currentForm: Form = this.getEmptyForm();
  showForm: boolean = false;
  isEditing: boolean = false;
  userRole: string | null = null;

  constructor(private formService: ServiceGenericService, private auth: AuthService) { }

  ngOnInit(): void {
    this.userRole = this.auth.getUserRole();
    console.log('ROL:', this.userRole); // ← Verifica si imprime "admin"
    this.loadForms();
  }


  // Helper para crear un formulario vacío
  getEmptyForm(): Form {
    return {
      id: 0,
      name: '',
      description: '',
      active: true
    };
  }

  loadForms(): void {
    this.formService.get<Form[]>('FormControllerPrueba').subscribe({
      next: data => this.forms = data,
      error: err => console.error('Error al cargar los formularios', err)
    });
  }

  // Maneja tanto la creación como la actualización
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
        this.forms.push(form);
        this.resetForm();
      },
      error: err => console.error('Error al agregar formulario', err)
    });
  }

  editForm(form: Form): void {
    this.isEditing = true;
    this.currentForm = { ...form }; // Copia del objeto para no modificar directamente
    this.showForm = true;           // Muestra el formulario
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
      next: () => this.forms = this.forms.filter(f => f.id !== id),
      error: err => console.error(`Error al eliminar (${mode}) formulario`, err)
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