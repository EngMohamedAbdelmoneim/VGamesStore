import { FormsModule, NgModel } from '@angular/forms';
import { Component, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { login } from '../../store/auth.actions';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login-page',
  imports: [CommonModule,FormsModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css',
  standalone: true,
})
export class LoginPageComponent {
  email = '';
  password = '';
  private router = inject(Router);

  constructor(private store: Store,private auth : AuthService) {}

  onLogin() {
    this.store.dispatch(login({ email: this.email, password: this.password }));
    if(this.auth.isLoggedIn()){
        this.router.navigate(['/games']);
    }

  }
}
