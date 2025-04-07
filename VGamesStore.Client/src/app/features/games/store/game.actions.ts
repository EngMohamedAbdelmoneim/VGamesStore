import { createAction, props } from '@ngrx/store';
import { Game } from '../../../core/models/game';

// Define actions for loading games --------------------------------------------------------------------------------
export const loadGames = createAction('[Game] Load Games');
export const loadGamesSuccess = createAction(
  '[Game] Load Games Success',
  props<{ games: Game[] }>()
);
export const loadGamesFailure = createAction(
  '[Game] Load Games Failure',
  props<{ error: string }>()
);

// Define actions for loading a single game --------------------------------------------------------------------------------
export const loadGameDetails = createAction(
  '[Game] Load Game Details',
  props<{ id: number }>()
);

export const loadGameDetailsSuccess = createAction(
  '[Game] Load Game Details Success',
  props<{ game: Game }>()
);

export const loadGameDetailsFailure = createAction(
  '[Game] Load Game Details Failure',
  props<{ error: string }>()
);
