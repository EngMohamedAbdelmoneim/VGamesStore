import { createSelector, createFeatureSelector } from '@ngrx/store';
import { GameState } from './game.reducer';

export const selectGameState = createFeatureSelector<GameState>('games');
export const selectAllGames = createSelector(selectGameState, state => state.games);
export const selectLoading = createSelector(selectGameState, state => state.loading);
export const selectError = createSelector(selectGameState, state => state.error);
