import { Filter } from './../../../core/models/filter';
import { createReducer, on } from "@ngrx/store";
import { Game } from "../../../core/models/game";
import * as SearchActions from './search.actions';

export interface SearchState {
  searchedGames: Game[];
  loading: boolean;
  error: string | null;
}

const initialState: SearchState = {
  searchedGames: [],
  loading: false,
  error: null
};

export const searchReducer = createReducer(
  initialState,
  on(SearchActions.searchGames, state => ({ ...state, loading: true })),
  on(SearchActions.searchGamesSuccess, (state, { searchedgames }) => ({ ...state, loading: false, searchedGames: searchedgames })),
  on(SearchActions.searchGamesFailure, (state, { error }) => ({ ...state, loading: false, error })),

  // Filter Actions
)

export const filterReducer = createReducer(
  initialState,
  on(SearchActions.applyingFilter, (state) => ({ ...state, loading: true })),
  on(SearchActions.loadFilteredGamesSuccess, (state, { games }) => ({
    ...state,
    loading: false,
    filteredGames: games,
  })),
  on(SearchActions.loadFilteredGamesFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
