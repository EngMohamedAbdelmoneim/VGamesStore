import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-home-page',
  imports: [CommonModule, RouterLink,FormsModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  standalone: true,
})
export class HomePageComponent {
  searchText: string = '';
}
