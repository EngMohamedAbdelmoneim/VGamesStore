import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SearchState } from "./search.reducer";


export const selectSearchState = createFeatureSelector<SearchState>('search');
export const selectSearchedGames = createSelector(selectSearchState, (state : SearchState) => state.searchedGames);
export const selectLoading = createSelector(selectSearchState, (state : SearchState) => state.loading);
export const selectError = createSelector(selectSearchState, (state : SearchState) => state.error);


export const selectFilteredGames = createSelector(
  selectSearchState,
  (state: SearchState) => state.searchedGames
);


