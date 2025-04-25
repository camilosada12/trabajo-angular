import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 

import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms'; 

@Component({
  selector: 'app-form-table',
  standalone: true, 
  imports: [CommonModule, MatButtonModule, FormsModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserTableComponent{
  }
