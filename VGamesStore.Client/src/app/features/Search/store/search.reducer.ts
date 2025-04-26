import { FilterDto } from '../../../core/models/filter-dto';
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

  // FilterDto Actions
)

export const filterReducer = createReducer(
  initialState,
  on(SearchActions.applyingFilterDto, (state) => ({ ...state, loading: true })),
  on(SearchActions.loadFilterDtoedGamesSuccess, (state, { games }) => ({
    ...state,
    loading: false,
    filteredGames: games,
  })),
  on(SearchActions.loadFilterDtoedGamesFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
