import { Routes } from '@angular/router';
import { GAME_ROUTES } from './features/games/routes';
import { HOME_ROUTES } from './features/home/routes';
import { Search_ROUTES } from './features/Search/routes';

export const appRoutes: Routes = [
  { path: '', children: HOME_ROUTES }, // Lazy loading Game routes
  { path: 'games', children: GAME_ROUTES }, // Lazy loading Game routes
  { path: 'search', children: Search_ROUTES }, // Lazy loading Game routes
  { path: '**', redirectTo: '' } // Catch-all redirect
];
