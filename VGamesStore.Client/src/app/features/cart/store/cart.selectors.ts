import { loadCartItems } from './cart.actions';
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { CartState } from './cart.reducer';

export const selectCartItemsState = createFeatureSelector<CartState>('cart');
export const selectAllcartItems = createSelector(selectCartItemsState, state => state.cartItems);
export const selectLoading = createSelector(selectCartItemsState, state => state.loading);
export const selectError = createSelector(selectCartItemsState, state => state.error);

// Selectors for adding a cart item
export const selectAddCartItemSuccess = createSelector(
  selectCartItemsState,
  state => state.addedCartItem
);
export const selectAddCartItemError = createSelector(
  selectCartItemsState,
  state => state.error
);
// Selectors for removing a cart item
export const selectRemoveCartItem = createSelector(
  selectCartItemsState,
  state => state.cartItems
);

