import { createReducer, on } from '@ngrx/store';
import * as GameActions from './game.actions';
import { Game } from '../../../core/models/game';

export interface GameState {
  games: Game[];
  loading: boolean;
  selectedGame: Game | null;
  error: string | null;
}

const initialState: GameState = {
  games: [],
  loading: false,
  selectedGame: null,
  error: null
};
// Define the reducer function for the list of games state
export const gamesReducer = createReducer(
  initialState,
  on(GameActions.loadGames, state => ({ ...state, loading: true })),
  on(GameActions.loadGamesSuccess, (state, { games }) => ({ ...state, loading: false, games })),
  on(GameActions.loadGamesFailure, (state, { error }) => ({ ...state, loading: false, error }))
);

// Define the reducer function for the selected game state
export const gameReducer = createReducer(
  initialState,
  on(GameActions.loadGameDetails, state => ({ ...state, loading: true })),
  on(GameActions.loadGameDetailsSuccess, (state, { game }) => ({
    ...state,
    loading: false,
    selectedGame: game
  })),
  on(GameActions.loadGameDetailsFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  }))


);




