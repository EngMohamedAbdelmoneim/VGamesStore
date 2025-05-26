import { Component, inject, Input } from '@angular/core';
import { WishlistItem } from '../../../../core/models/wishlist-item';
import { imagePath } from '../../../../utils/imagePath';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import * as WishlistActions from '../../../../features/wishlist/store/wishlist.actions';

@Component({
  selector: 'app-wishlist-item-card',
  imports: [CommonModule],
  templateUrl: './wishlist-item-card.component.html',
  styleUrl: './wishlist-item-card.component.css'
})
export class WishlistItemCardComponent {
  @Input() wishlistItem: WishlistItem | null = null;
  private store = inject(Store);

  removeFromWishlist() {
    const wishlistItem: WishlistItem = {
      gameId: this.wishlistItem!.gameId,
      gameName: this.wishlistItem!.gameName,
      imageUrl: this.wishlistItem!.imageUrl,
      price: this.wishlistItem!.price,
    };
    this.store.dispatch(WishlistActions.removeWishlistItem({ wishlistItem }));
    console.log(`Game with ID ${this.wishlistItem?.gameId} removed from wishlist.`);
  }
  getImageUrl(Url: any | null): any {
    return imagePath(Url);
  }
}
