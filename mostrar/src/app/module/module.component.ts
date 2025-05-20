  import { Component } from '@angular/core';
  import { FormsModule } from '@angular/forms';
  import { ServiceGenericService } from '../service-generic.service';
  import { MatButtonModule } from '@angular/material/button';
  import { CommonModule } from '@angular/common';
  import { Module } from '../Interfaces/module';
import { AuthService } from '../services/auth.service';

  @Component({
    selector: 'app-module',
    imports: [CommonModule, MatButtonModule, FormsModule],
    templateUrl: './module.component.html',
    styleUrl: './module.component.css'
  })
  export class ModuleComponent {
    Modules: Module[] = [];
      currentModule: Module = this.getEmptyModule();
      showModule: boolean = false;
      isEditing: boolean = false;
      userRole : string | null = null;
      
      constructor(private ModuleServices: ServiceGenericService, private auth : AuthService) {}
    
      ngOnInit(): void {
        this.loadModule();
        this.userRole = this.auth.getUserRole();
      }
    
      // Helper para crear un formulario vacío
      getEmptyModule(): Module {
        return {
          id: 0,
          name: '',
          description: '',
          active: true
        };
      }
    
      loadModule(): void {
        this.ModuleServices.get<Module[]>('Module').subscribe({
          next: data => this.Modules = data,
          error: err => console.error('Error al cargar los formularios', err)
        });
      }
    
      // Maneja tanto la creación como la actualización
      submitModule(): void {
        if (this.isEditing) {
          this.updateModule();
        } else {
          this.addModule();
        }
      }
    
      addModule(): void {
        this.ModuleServices.post<Module>('Module', this.currentModule).subscribe({
          next: module => {
            this.Modules.push(module);
            this.resetForm();
          },
          error: err => console.error('Error al agregar formulario', err)
        });
      }
    
      editModule(module: Module): void {
        this.isEditing = true;
        this.currentModule = { ...module }; // Copia del objeto para no modificar directamente
        this.showModule = true;           // Muestra el formulario
      }  
    
      updateModule(): void {
        this.ModuleServices.put<Module>('Module', this.currentModule).subscribe({
          next: () => {
            this.loadModule();
            this.resetForm();
          },
          error: err => console.error('Error al actualizar formulario', err)
        });
      }
      
    
      deleteModule(id: number, mode: 'fisico' | 'Logical' = 'fisico'): void {
        this.ModuleServices.delete<Module>('Module', id, mode).subscribe({
          next: () => this.Modules = this.Modules.filter(f => f.id !== id),
          error: err => console.error(`Error al eliminar (${mode}) formulario`, err)
        });
      }
      
    
      toggleForm(mode: 'create' | 'edit'): void {
        if (mode === 'create') {
          this.isEditing = false;
          this.currentModule = this.getEmptyModule();
        }
        this.showModule = !this.showModule;
      }
    
      cancelForm(): void {
        this.resetForm();
      }
    
      resetForm(): void {
        this.showModule = false;
        this.isEditing = false;
        this.currentModule = this.getEmptyModule();
      }
    }

