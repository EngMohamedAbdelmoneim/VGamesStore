import { Routes } from '@angular/router';
import { GAME_ROUTES } from './features/games/routes';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'games', pathMatch: 'full' },
  { path: 'games', children: GAME_ROUTES }, // Lazy loading Game routes
  { path: '**', redirectTo: 'games' } // Catch-all redirect
];
