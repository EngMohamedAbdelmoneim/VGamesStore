import { CartItem } from './../../../core/models/cart-item';
import { CartService } from './../../../core/services/cart.service';
import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { addCartItem, addCartItemFailure, addCartItemSuccess, loadCartItems, loadCartItemsFailure, loadCartItemsSuccess, removeCart, removeCartItem, removeCartItemFailure, removeCartItemSuccess } from './cart.actions';

@Injectable()
export class CartEffects {
  private actions$ = inject(Actions);
  private cartService = inject(CartService);

  loadCartItems$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadCartItems),
      mergeMap(() =>
        this.cartService.createAndGetCart().pipe(
          map((cart) => loadCartItemsSuccess({ cartItems: cart.items })),
          catchError((error) => of(loadCartItemsFailure({ error: error.message })))
        )
      )
    )
  );
  //Effect for adding a cart item
  addCartItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addCartItem),
      mergeMap(({ cartItem }) =>
        this.cartService.addOrUpdateCartItem(cartItem).pipe(
          map((addedCartItem) => addCartItemSuccess({ cartItem: addedCartItem })),
          catchError((error) => of(addCartItemFailure({ error: error.message })))
        )
      )
    )
  );
  // Effect for removing a cart and its item
removeCart$ = createEffect(() =>
    this.actions$.pipe(
      ofType(removeCart),
      mergeMap(() =>
        this.cartService.removeCart().pipe(
          map(() => removeCartItemSuccess({ cartItems: [] })), // Assuming the cart is cleared
          catchError((error) => of(removeCartItemFailure({ error: error.message })))
        )
      )
    )
  );

  removeCartItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(removeCartItem),
      mergeMap(({ cartItem }) =>
        this.cartService.RemoveCartItem(cartItem.gameId).pipe(
          map((cart) => removeCartItemSuccess({ cartItems: cart.items })),
          catchError((error) => of(removeCartItemFailure({ error: error.message })))
        )
      )
    )
  );
}
