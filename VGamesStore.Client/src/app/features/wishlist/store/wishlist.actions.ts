import { createAction, props } from '@ngrx/store';
import { WishlistItem } from '../../../core/models/wishlist-item';

// Define actions for loading games --------------------------------------------------------------------------------
export const loadWishlistItems = createAction('[Wishlist] Load Wishlist Items');
export const loadWishlistItemsSuccess = createAction(
  '[Wishlist] Load Wishlist Success',
  props<{ wishlistItems: WishlistItem[] }>()
);
export const loadWishlistItemsFailure = createAction(
  '[Wishlist] Load Wishlist Items Failure',
  props<{ error: string }>()
);

// Define actions for adding a wishlist item --------------------------------------------------------------------------
export const addWishlistItem = createAction(
  '[Wishlist] Add Wishlist Item',
  props<{ wishlistItem: WishlistItem }>()
);
export const addWishlistItemSuccess = createAction(
  '[Wishlist] Add Wishlist Item Success',
  props<{ wishlistItem: WishlistItem }>()
);
export const addWishlistItemFailure = createAction(
  '[Wishlist] Add Wishlist Item Failure',
  props<{ error: string }>()
);

// Define actions for removing a wishlist and its items ------------------------------------------------------------------------
export const removeWishlist = createAction(
  '[Wishlist] Remove Wishlist'
);

export const removeWishlistItem = createAction(
  '[Wishlist] Remove Wishlist Item',
  props<{ wishlistItem: WishlistItem }>()
);
export const removeWishlistItemSuccess = createAction(
  '[Wishlist] Remove Wishlist Item Success',
  props<{ wishlistItems: WishlistItem[] }>()
);
export const removeWishlistItemFailure = createAction(
  '[Wishlist] Remove Wishlist Item Failure',
  props<{ error: string }>()
);
