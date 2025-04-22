import { Component } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html', 
  styleUrls: ['./user.component.css']   
})
export class UserComponent {
  users = [
    { name: 'Juan Pérez', email: 'juan@example.com' },
    { name: 'Ana García', email: 'ana@example.com' },
    { name: 'Luis Torres', email: 'luis@example.com' }
  ];
}
