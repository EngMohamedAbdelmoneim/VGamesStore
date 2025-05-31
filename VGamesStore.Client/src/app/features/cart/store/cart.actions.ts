import { createAction, props } from '@ngrx/store';
import { CartItem } from '../../../core/models/cart-item';

// Define actions for loading games --------------------------------------------------------------------------------
export const loadCartItems = createAction('[Cart] Load Cart Items');
export const loadCartItemsSuccess = createAction(
  '[Cart] Load Cart Success',
  props<{ cartItems: CartItem[] }>()
);
export const loadCartItemsFailure = createAction(
  '[Cart] Load Cart Items Failure',
  props<{ error: string }>()
);

// Define actions for adding a cart item --------------------------------------------------------------------------
export const addCartItem = createAction(
  '[Cart] Add Cart Item',
  props<{ cartItem: CartItem }>()
);
export const addCartItemSuccess = createAction(
  '[Cart] Add Cart Item Success',
  props<{ cartItem: CartItem }>()
);
export const addCartItemFailure = createAction(
  '[Cart] Add Cart Item Failure',
  props<{ error: string }>()
);

// Define actions for removing a cart and its items ------------------------------------------------------------------------
export const removeCart = createAction(
  '[Cart] Remove Cart'
);

export const removeCartItem = createAction(
  '[Cart] Remove Cart Item',
  props<{ cartItem: CartItem }>()
);
export const removeCartItemSuccess = createAction(
  '[Cart] Remove Cart Item Success',
  props<{ cartItems: CartItem[] }>()
);
export const removeCartItemFailure = createAction(
  '[Cart] Remove Cart Item Failure',
  props<{ error: string }>()
);
