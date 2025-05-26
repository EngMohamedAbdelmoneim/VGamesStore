import { WishlistItem } from './../../../core/models/wishlist-item';
import { WishlistService } from './../../../core/services/wishlist.service';
import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { addWishlistItem, addWishlistItemFailure, addWishlistItemSuccess, loadWishlistItems, loadWishlistItemsFailure, loadWishlistItemsSuccess, removeWishlistItem, removeWishlistItemFailure, removeWishlistItemSuccess } from './wishlist.actions';

@Injectable()
export class WishlistEffects {
  private actions$ = inject(Actions);
  private wishlistService = inject(WishlistService);

  loadWishlistItems$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadWishlistItems),
      mergeMap(() =>
        this.wishlistService.createAndGetWishlist().pipe(
          map((wishlist) => loadWishlistItemsSuccess({ wishlistItems: wishlist.items })),
          catchError((error) => of(loadWishlistItemsFailure({ error: error.message })))
        )
      )
    )
  );
  //Effect for adding a wishlist item
  addWishlistItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addWishlistItem),
      mergeMap(({ wishlistItem }) =>
        this.wishlistService.addOrUpdateWishlistItem(wishlistItem).pipe(
          map((addedWishlistItem) => addWishlistItemSuccess({ wishlistItem: addedWishlistItem })),
          catchError((error) => of(addWishlistItemFailure({ error: error.message })))
        )
      )
    )
  );
  // Effect for removing a wishlist and its item
removeWishlist$ = createEffect(() =>
    this.actions$.pipe(
      ofType(removeWishlistItem),
      mergeMap(() =>
        this.wishlistService.removeWishlist().pipe(
          map(() => removeWishlistItemSuccess({ wishlistItems: [] })), // Assuming the wishlist is cleared
          catchError((error) => of(removeWishlistItemFailure({ error: error.message })))
        )
      )
    )
  );

  removeWishlistItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(removeWishlistItem),
      mergeMap(({ wishlistItem }) =>
        this.wishlistService.RemoveWishlistItem(wishlistItem.gameId).pipe(
          map((wishlist) => removeWishlistItemSuccess({ wishlistItems: wishlist.items })),
          catchError((error) => of(removeWishlistItemFailure({ error: error.message })))
        )
      )
    )
  );
}
