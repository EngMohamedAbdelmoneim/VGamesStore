import { createReducer, on } from '@ngrx/store';
import { CartItem } from '../../../core/models/cart-item';
import * as CartActions from './cart.actions';
export interface CartState {
  cartItems: CartItem[];
  addedCartItem: CartItem | null;
  removedCartItem: CartItem | null;
  loading: boolean;
  error: string | null;
}

const initialState: CartState = {
  cartItems: [],
  addedCartItem: null,
  removedCartItem: null,
  loading: false,
  error: null
};
// Define the reducer function for the list of games state
export const cartReducer = createReducer(
  initialState,
  on(CartActions.loadCartItems, state => ({ ...state, loading: true })),
  on(CartActions.loadCartItemsSuccess, (state, { cartItems }) => ({ ...state, loading: false, cartItems })),
  on(CartActions.loadCartItemsFailure, (state, { error }) => ({ ...state, loading: false, error })),

  // Actions for adding a cart item
  on(CartActions.addCartItemSuccess, (state, { cartItem }) => ({
    ...state,
    addedCartItem: cartItem,
  })),
  on(CartActions.addCartItemFailure, (state, { error }) => ({ ...state, error })),

  // Actions for removing a cart item
  on(CartActions.removeCartItem, state => ({ ...state, loading: true })),
  on(CartActions.removeCartItemSuccess,  (state, { cartItems }) => ({ ...state, loading: false, cartItems })),

);
