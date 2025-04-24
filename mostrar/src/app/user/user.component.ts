import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { Form } from '../Interfaces/form';
import { ServicesFormService } from '../services/services-form.service';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms'; // Asegúrate de importar FormsModule
import { ServiceGenericService } from '../service-generic.service';


@Component({
  selector: 'app-form-table',
  standalone: true, 
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class FormTableComponent implements OnInit {
  forms: Form[] = [];
  selectedForm: Form | null = null;
  newForm: Form = {
    id: 0,
    name: '',
    description: '',
    active: true
  };
  
  showCreateForm: boolean = false;  // Nueva variable para controlar la visibilidad del formulario

  constructor(private formService: ServiceGenericService) {}

  ngOnInit(): void {
    this.loadForms();
  }

  loadForms(): void {
    this.formService.get<Form[]>('FormControllerPrueba').subscribe({
      next: data => this.forms = data,
      error: err => console.error('Error al cargar los formularios', err)
    });
  }

  // addForm(): void {
  //   this.formService.create(this.newForm).subscribe({
  //     next: (form) => {
  //       this.forms.push(form); // Agrega el nuevo form a la lista
  //       this.newForm = { id: 0, name: '', description: '', active: true }; // Limpia el form
  //       this.showCreateForm = false; // Oculta el formulario
  //     },
  //     error: err => console.error('Error al agregar formulario', err)
  //   });
  // }
  

  // editForm(form: Form): void {
  //   this.selectedForm = { ...form };
  // }

  // updateForm(): void {
  //   if (this.selectedForm) {
  //     this.formService.update(this.selectedForm.id, this.selectedForm).subscribe({
  //       next: updatedForm => {
  //         const index = this.forms.findIndex(f => f.id === updatedForm.id);
  //         if (index > -1) this.forms[index] = updatedForm;
  //         this.selectedForm = null;
  //       },
  //       error: err => console.error('Error al actualizar formulario', err)
  //     });
  //   }
  // }

  // deleteForm(id: number): void {
  //   this.formService.delete(id).subscribe({
  //     next: () => this.forms = this.forms.filter(f => f.id !== id),
  //     error: err => console.error('Error al eliminar formulario', err)
  //   });
  // }

  // deleteFormLogic(id: number): void {
  //   this.formService.deleteLogic(id).subscribe({
  //     next: () => this.forms = this.forms.filter(f => f.id !== id),
  //     error: err => console.error('Error al eliminar lógicamente', err)
  //   });
  // }

  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm; // Cambiar el estado de visibilidad
  }

  
}

