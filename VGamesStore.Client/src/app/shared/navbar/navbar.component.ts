import { Component, inject } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule,MatToolbarModule, MatButtonModule, MatIconModule,RouterLink],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  searchText: string = '';
  private router = inject(Router);

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'custom-icon',
      sanitizer.bypassSecurityTrustResourceUrl('http://localhost:4200/assets/VGameLogo.svg')
    );
  }
  goToSearch() {
    if (this.searchText && this.searchText.trim()) {
      this.router.navigate(['/search', this.searchText]);
    }
  }
}

