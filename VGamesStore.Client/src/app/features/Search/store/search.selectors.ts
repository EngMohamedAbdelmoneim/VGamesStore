import { createFeatureSelector, createSelector } from '@ngrx/store';
import { SearchState } from './search.reducer';

export const selectSearchState = createFeatureSelector<SearchState>('search');

export const selectSearchedGames = createSelector(
  selectSearchState,
  (state) => state.searchedGames
);

export const selectSearchLoading = createSelector(
  selectSearchState,
  (state) => state.loading
);

export const selectSearchError = createSelector(
  selectSearchState,
  (state) => state.error
);

export const selectSearchOrigin = createSelector(
  selectSearchState,
  (state) => state.origin
);
