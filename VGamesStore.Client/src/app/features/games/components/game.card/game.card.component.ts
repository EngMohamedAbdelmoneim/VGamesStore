import { CartService } from './../../../../core/services/cart.service';
import { CommonModule } from '@angular/common';
import { imagePath } from '../../../../utils/imagePath';
import { Component, inject, Input } from '@angular/core';
import { Game } from '../../../../core/models/game';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Store } from '@ngrx/store';
import { CartItem } from '../../../../core/models/cart-item';
import * as CartActions from '../../../../features/cart/store/cart.actions';
import { Observable } from 'rxjs';
import { selectAddCartItemError, selectAddCartItemSuccess } from '../../../cart/store/cart.selectors';

@Component({
  selector: 'app-game-card',
  imports: [CommonModule, RouterLink, MatButtonModule, MatTooltipModule, MatIconModule],
  templateUrl: './game.card.component.html',
  standalone: true,
  styleUrl: './game.card.component.css'
})

export class GameCardComponent {
  @Input() game!: Game;
  private store = inject(Store);

  addSuccess$: Observable<CartItem | null> = this.store.select(selectAddCartItemSuccess);
  error$: Observable<string | null> = this.store.select(selectAddCartItemError);

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'add-to-wishlist',
      sanitizer.bypassSecurityTrustResourceUrl('http://localhost:4200/assets/add-to-wishlist.svg')
    );
  }
  getImageUrl(Url: any | null): any {
    return imagePath(Url);
  }

  addToCart() {
    const cartItem: CartItem = {
      gameId: this.game.id,
      gameName: this.game.title,
      imageUrl: this.game.imageUrl,
      price: this.game.price,
    };

    this.store.dispatch(CartActions.addCartItem({ cartItem }));
    console.log(`Game with ID ${this.game.id} added to cart.`);
  }
}
