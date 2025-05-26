import { createReducer, on } from '@ngrx/store';
import { WishlistItem } from '../../../core/models/wishlist-item';
import * as WishlistActions from './wishlist.actions';
export interface WishlistState {
  wishlistItems: WishlistItem[];
  addedWishlistItem: WishlistItem | null;
  removedWishlistItem: WishlistItem | null;
  loading: boolean;
  error: string | null;
}

const initialState: WishlistState = {
  wishlistItems: [],
  addedWishlistItem: null,
  removedWishlistItem: null,
  loading: false,
  error: null
};
// Define the reducer function for the list of games state
export const wishlistReducer = createReducer(
  initialState,
  on(WishlistActions.loadWishlistItems, state => ({ ...state, loading: true })),
  on(WishlistActions.loadWishlistItemsSuccess, (state, { wishlistItems }) => ({ ...state, loading: false, wishlistItems })),
  on(WishlistActions.loadWishlistItemsFailure, (state, { error }) => ({ ...state, loading: false, error })),

  // Actions for adding a wishlist item
  on(WishlistActions.addWishlistItemSuccess, (state, { wishlistItem }) => ({
    ...state,
    addedWishlistItem: wishlistItem,
  })),
  on(WishlistActions.addWishlistItemFailure, (state, { error }) => ({ ...state, error })),

  // Actions for removing a wishlist item
  on(WishlistActions.removeWishlistItem, state => ({ ...state, loading: true })),
  on(WishlistActions.removeWishlistItemSuccess,  (state, { wishlistItems }) => ({ ...state, loading: false, wishlistItems })),

);
