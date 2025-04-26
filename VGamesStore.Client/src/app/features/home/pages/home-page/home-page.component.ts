import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-home-page',
  imports: [CommonModule,FormsModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  standalone: true,
})
export class HomePageComponent {
  searchText: string = '';
  private router = inject(Router);
  goToSearch() {
    if (this.searchText && this.searchText.trim()) {
      this.router.navigate(['/search', this.searchText]);
    }
  }
}
