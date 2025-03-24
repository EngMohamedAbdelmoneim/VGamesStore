import { createAction, props } from '@ngrx/store';
import { Game } from '../../../core/models/game';

export const loadGames = createAction('[Game] Load Games');
export const loadGamesSuccess = createAction('[Game] Load Games Success', props<{ games: Game[] }>());
export const loadGamesFailure = createAction('[Game] Load Games Failure', props<{ error: string }>());
