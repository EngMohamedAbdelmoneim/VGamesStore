import { Routes } from '@angular/router';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { AuthGuard } from '../auth/auth.guard';


export const Cart_ROUTES: Routes = [
  { path: '',
    component: CartPageComponent,
    canActivate: [AuthGuard] },
];
