import { createReducer, on } from '@ngrx/store';
import * as SearchActions from './search.actions';
import { Game } from '../../../core/models/game';

export interface SearchState {
  searchedGames: Game[];
  loading: boolean;
  error: string | null;
  origin: 'keyword' | 'genre' | 'filter' | null;
}

const initialState: SearchState = {
  searchedGames: [],
  loading: false,
  error: null,
  origin: null
};

export const searchReducer = createReducer(
  initialState,
  on(
    SearchActions.searchGames,
    SearchActions.searchGamesByGenresName,
    SearchActions.applyingFilterDto,
    (state) => ({ ...state, loading: true, error: null })
  ),
  on(SearchActions.searchGamesSuccess, (state, { searchedgames, origin }) => ({
    ...state,
    searchedGames: searchedgames,
    loading: false,
    origin,
    error: null
  })),
  on(SearchActions.searchGamesFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error
  }))
);
