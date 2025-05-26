import { Component, inject, Input } from '@angular/core';
import { CartItem } from '../../../../core/models/cart-item';
import { imagePath } from '../../../../utils/imagePath';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import * as CartActions from '../../../../features/cart/store/cart.actions';

@Component({
  selector: 'app-cart-item-card',
  imports: [CommonModule],
  templateUrl: './cart-item-card.component.html',
  styleUrl: './cart-item-card.component.css'
})
export class CartItemCardComponent {
  @Input() cartItem: CartItem | null = null;
  private store = inject(Store);

  removeFromCart() {
    const cartItem: CartItem = {
      gameId: this.cartItem!.gameId,
      gameName: this.cartItem!.gameName,
      imageUrl: this.cartItem!.imageUrl,
      price: this.cartItem!.price,
    };
    this.store.dispatch(CartActions.removeCartItem({ cartItem }));
    console.log(`Game with ID ${this.cartItem?.gameId} removed from cart.`);
  }
  getImageUrl(Url: any | null): any {
    return imagePath(Url);
  }
}
