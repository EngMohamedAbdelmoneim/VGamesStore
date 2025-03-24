import { Routes } from '@angular/router';
import { GameListComponent } from './pages/game-list/game-list.component';
import { GameDetailsComponent } from './pages/game-details/game-details.component';

export const GAME_ROUTES: Routes = [
  { path: '', component: GameListComponent },
  { path: ':id', component: GameDetailsComponent }
];
