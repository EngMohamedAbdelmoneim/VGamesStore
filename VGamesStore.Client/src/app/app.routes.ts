import { Routes } from '@angular/router';
import { GAME_ROUTES } from './features/games/routes';
import { HOME_ROUTES } from './features/home/routes';
import { Search_ROUTES } from './features/Search/routes';
import { Cart_ROUTES } from './features/cart/routes';
import { Wishlist_ROUTES } from './features/wishlist/routes';
import { AUTH_ROUTES } from './features/auth/routes';


export const appRoutes: Routes = [
  { path: '', children: HOME_ROUTES }, // Lazy loading Game routes
  { path: 'games', children: GAME_ROUTES }, // Lazy loading Game routes
  { path: 'search', children: Search_ROUTES }, // Lazy loading search routes
  { path: 'cart', children: Cart_ROUTES }, // Lazy loading cart routes
  { path: 'wishlist', children: Wishlist_ROUTES }, // Lazy loading wishlist routes
  { path: 'auth', children: AUTH_ROUTES }, // Lazy loading auth routes
  { path: '**', redirectTo: '' } // Catch-all redirect
];
