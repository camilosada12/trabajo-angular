import { Component, OnInit } from '@angular/core';
import { ServiceGenericService } from '../service-generic.service';
import { Person } from '../Interfaces/person';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-person',
  standalone: true, // Agrega esta línea
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './person.component.html',
  styleUrl: './person.component.css'
})
export class PersonComponent implements OnInit {
 
  Persons: Person[] = [];
    currentPerson: Person = this.getEmptyPerson();
    showPerson: boolean = false;
    isEditing: boolean = false;
    
    constructor(private PersonServices: ServiceGenericService) {}
  
    ngOnInit(): void {
      this.loadPersons();
    }
  
    // Helper para crear un formulario vacío
    getEmptyPerson(): Person {
      return {
        id: 0,
        firstName : '',
        lastName: '',
        phonenumber: '',
        active: true
      };
    }
  
    loadPersons(): void {
      this.PersonServices.get<Person[]>('Person').subscribe({
        next: data => {
          console.log('Datos recibidos:', data);  // Añade esta línea
          this.Persons = data;
        },
        error: err => console.error('Error al cargar los formularios', err)
      });
    }
    // Maneja tanto la creación como la actualización
    submitPerson(): void {
      if (this.isEditing) {
        this.updatePerson();
      } else {
        this.addPerson();
      }
    }
  
    addPerson(): void {
      // Asegurar que phoneNumber sea un número
      this.currentPerson.phonenumber = (this.currentPerson.phonenumber);
      
      this.PersonServices.post<Person>('Person', this.currentPerson).subscribe({
        next: module => {
          this.Persons.push(module);
          this.resetPerson();
        },
        error: err => console.error('Error al agregar formulario', err)
      });
    }

    editPerson(module: Person): void {
      this.isEditing = true;
      this.currentPerson = { ...module }; // Copia del objeto para no modificar directamente
      this.showPerson = true;           // Muestra el formulario
    }  
  
    updatePerson(): void {
      // Asegurar que phoneNumber sea un número
      this.currentPerson.phonenumber = (this.currentPerson.phonenumber);
      
      this.PersonServices.put<Person>('Person', this.currentPerson).subscribe({
        next: () => {
          this.loadPersons();
          this.resetPerson();
        },
        error: err => console.error('Error al actualizar formulario', err)
      });
    }
    
  
    deletePerson(id: number): void {
      this.PersonServices.delete<Person>('Person', id).subscribe({
        next: () => this.Persons = this.Persons.filter(f => f.id !== id),
        error: err => console.error('Error al eliminar formulario', err)
      });
    }
  
    deletePersonLogic(id: number): void {
      this.PersonServices.deleteLogic<Person>('Person', id).subscribe({
        next: () => this.Persons = this.Persons.filter(f => f.id !== id),
        error: err => console.error('Error al eliminar lógicamente', err)
      });
    }
    
  
    togglePerson(mode: 'create' | 'edit'): void {
      if (mode === 'create') {
        this.isEditing = false;
        this.currentPerson = this.getEmptyPerson();
      }
      this.showPerson = !this.showPerson;
    }
  
    cancelPerson(): void {
      this.resetPerson();
    }
  
    resetPerson(): void {
      this.showPerson = false;
      this.isEditing = false;
      this.currentPerson = this.getEmptyPerson();
    }

}
