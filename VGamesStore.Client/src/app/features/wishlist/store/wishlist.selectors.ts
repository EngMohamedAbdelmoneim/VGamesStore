import { loadWishlistItems } from './wishlist.actions';
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { WishlistState } from './wishlist.reducer';

export const selectWishlistItemsState = createFeatureSelector<WishlistState>('wishlist');
export const selectAllwishlistItems = createSelector(selectWishlistItemsState, state => state.wishlistItems);
export const selectLoading = createSelector(selectWishlistItemsState, state => state.loading);
export const selectError = createSelector(selectWishlistItemsState, state => state.error);

// Selectors for adding a wishlist item
export const selectAddWishlistItemSuccess = createSelector(
  selectWishlistItemsState,
  state => state.addedWishlistItem
);
export const selectAddWishlistItemError = createSelector(
  selectWishlistItemsState,
  state => state.error
);
// Selectors for removing a wishlist item
export const selectRemoveWishlistItem = createSelector(
  selectWishlistItemsState,
  state => state.wishlistItems
);

