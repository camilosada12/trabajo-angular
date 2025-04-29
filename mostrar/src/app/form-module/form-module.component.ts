import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { form_Module } from '../Interfaces/form_Module';
import { ServiceGenericService } from '../service-generic.service';

@Component({
  selector: 'app-form-module',
  standalone: true,
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './form-module.component.html',
  styleUrl: './form-module.component.css'
})
export class FormModuleComponent {
  formModules: form_Module[] = [];
  currentFormModule: form_Module = this.getEmptyFormModule();
  showFormModule: boolean = false;
  isEditing: boolean = false;
  forms: { id: number, name: string }[] = [];
  modules: { id: number, name: string }[] = [];

  constructor(private serviceModuleFormGeneric: ServiceGenericService) { }

  ngOnInit(): void {
    this.loadFormModules();
    this.loadForm();
    this.loadModules();
  }

  getEmptyFormModule(): form_Module {
    return {
      id: 0,
      formId: 0,
      moduleId: 0,
      isDeleted: false
    };
  }

  loadForm(): void {
    this.serviceModuleFormGeneric.get<{ id: number, formname: string }[]>('FormControllerPrueba').subscribe({
      next: data => {
        this.forms = data.map(f => ({
          id: f.id,
          name: f.formname
        }));
      },
      error: err => console.error('error al cargar usuarios', err)
    });
  }

  loadModules(): void {
    this.serviceModuleFormGeneric.get<{ id: number, modulename: string }[]>('Module').subscribe({
      next: data => {
        this.modules = data.map(m => ({
          id: m.id,
          name: m.modulename
        }));
      },
      error: err => console.error('error al cargar roles', err)
    });
  }

  loadFormModules(): void {
    this.serviceModuleFormGeneric.get<form_Module[]>('FormModule').subscribe({
      next: data => {
        // Asegurarse de que todos los formModules tienen la propiedad isDeleted definida
        this.formModules = data.map(fm => ({
          ...fm,
          isDeleted: fm.isDeleted === true // Garantizar que isDeleted sea booleano
        }));
        // Asegurarnos de que los nombres se carguen correctamente
        this.updateFormModuleNames();
      },
      error: err => console.error('error al cargar form_Module', err)
    });
  }

  // Método para actualizar los nombres después de cargar los formModules
  updateFormModuleNames(): void {
    if (this.forms.length > 0 && this.modules.length > 0) {
      // Si ya tenemos los forms y modules cargados, actualizar los nombres
      this.formModules.forEach(fm => {
        const form = this.forms.find(f => f.id === fm.formId);
        const module = this.modules.find(m => m.id === fm.moduleId);
        
        if (form) {
          fm.formname = form.name;
        }
        
        if (module) {
          fm.modulename = module.name;
        }
      });
    }
  }

  submitFormModule(): void {
    const existingFormModule = this.formModules.find(fm =>
      fm.formId === this.currentFormModule.formId
    );

    if (existingFormModule && !this.isEditing) {
      // si ya existe y no estamos editando, simplemente actualizamos el rol
      this.currentFormModule.id = existingFormModule.id; // usamos el id existente
      this.isEditing = true;
      this.updateFormModule();
    } else if (this.isEditing) {
      this.updateFormModule();
    } else {
      this.addFormModule();
    }
  }

  addFormModule(): void {
    // Obtener los nombres correspondientes antes de enviar
    const form = this.forms.find(f => f.id === this.currentFormModule.formId);
    const module = this.modules.find(m => m.id === this.currentFormModule.moduleId);
    
    // Crear el payload para enviar al backend
    const payload = {
      formId: this.currentFormModule.formId,
      moduleId: this.currentFormModule.moduleId,
      formname: form?.name,
      modulename: module?.name
    };
  
    this.serviceModuleFormGeneric.post<form_Module>('FormModule', payload).subscribe({
      next: formModule => {
        // Agregar nombres correctos al objeto devuelto por el API antes de agregarlo al array local
        const newFormModule = {
          ...formModule,
          formname: form?.name || 'Desconocido',
          modulename: module?.name || 'Desconocido'
        };
        
        this.formModules.push(newFormModule);
        this.resetFormModule();
      },
      error: err => {
        console.error('Error al agregar FormModule', err);
      }
    });
  }

  editFormModule(Form_Module: form_Module): void {
    this.currentFormModule = {
      ...Form_Module, // copiamos todo el objeto rolUser (id, userId, rolId, etc)
    };
    this.isEditing = true;
    this.showFormModule = true;
  }

  updateFormModule(): void {
    // Obtener los nombres correspondientes antes de enviar
    const form = this.forms.find(f => f.id === this.currentFormModule.formId);
    const module = this.modules.find(m => m.id === this.currentFormModule.moduleId);
    
    // Agregar los nombres al objeto que se enviará para actualizar
    const updatePayload = {
      ...this.currentFormModule,
      formname: form?.name,
      modulename: module?.name
    };

    this.serviceModuleFormGeneric.put<form_Module>('FormModule', updatePayload).subscribe({
      next: () => {
        this.loadFormModules();
        this.resetFormModule();
      },
      error: err => console.error('error al actualizar rol_user', err)
    });
  }

  deleteFormModule(id: number): void {
    this.serviceModuleFormGeneric.delete<form_Module>('FormModule', id).subscribe({
      next: () => this.formModules = this.formModules.filter(fm => fm.id !== id),
      error: err => console.error('error al eliminar FormModule', err)
    });
  }

  deleteFormModuleLogic(id: number): void {
    this.serviceModuleFormGeneric.deleteLogic<form_Module>('FormModule', id).subscribe({
      next: () => {
        const formModules = this.formModules.find(fm => fm.id === id);
        if (formModules) {
          formModules.isDeleted = true;
        }
      },
      error: err => console.error('error al eliminar lógicamente FormModule', err)
    });
  }

  toggleFormModule(mode: 'create' | 'edit'): void {
    if (mode === 'create') {
      this.isEditing = false;
      this.currentFormModule = this.getEmptyFormModule();
    }
    this.showFormModule = !this.showFormModule;
  }

  cancelFormModule(): void {
    this.resetFormModule();
  }

  resetFormModule(): void {
    this.showFormModule = false;
    this.isEditing = false;
    this.currentFormModule = this.getEmptyFormModule();
  }

  getFormName(formId: number): string {
    // Primero intentamos obtener el nombre del propio objeto formModule
    const formModule = this.formModules.find(fm => fm.formId === formId);
    if (formModule && formModule.formname) {
      return formModule.formname;
    }
    
    // Si no está en el objeto, buscamos en la lista de forms
    const form = this.forms.find(f => f.id === formId);
    return form ? form.name : 'desconocido';
  }

  getModuleName(moduleId: number): string {
    // Primero intentamos obtener el nombre del propio objeto formModule
    const formModule = this.formModules.find(fm => fm.moduleId === moduleId);
    if (formModule && formModule.modulename) {
      return formModule.modulename;
    }
    
    // Si no está en el objeto, buscamos en la lista de modules
    const module = this.modules.find(m => m.id === moduleId);
    return module ? module.name : 'desconocido';
  }

  get availableFormModules() {
    // Filtrar formularios que no están asignados o el actual que estamos editando
    return this.forms.filter(form => 
      // Mostrar si no está asignado a ningún módulo (no está en formModules)
      !this.formModules.some(formModule => 
        formModule.formId === form.id && 
        !formModule.isDeleted && 
        formModule.id !== this.currentFormModule.id
      ) ||
      // O si es el formulario que estamos editando actualmente
      form.id === this.currentFormModule.formId
    );
  }
  
  get availableModules() {
    // Para módulos, mostramos todos ya que un módulo puede estar en múltiples formularios
    return this.modules;
  }
}