import { removeCart } from './../../store/cart.actions';
import { CartItem } from './../../../../core/models/cart-item';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectAllcartItems, selectError, selectLoading } from '../../store/cart.selectors';
import * as CartActions from '../../store/cart.actions';
import { CommonModule } from '@angular/common';
import { CartItemCardComponent } from "../../components/cart-item-card/cart-item-card.component";
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-cart-page',
  imports: [CommonModule, CartItemCardComponent, MatIconModule],
  templateUrl: './cart-page.component.html',
  styleUrl: './cart-page.component.css'
})
export class CartPageComponent implements OnInit {
  private store = inject(Store);

  textArray = 'Your Cart'.split('');
  arrows = Array(3); // Three arrows

  CartItems$: Observable<CartItem[]> = this.store.select(selectAllcartItems);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);

  ngOnInit(): void {
    this.store.dispatch(CartActions.loadCartItems());
    console.log(this.CartItems$.forEach((game) => console.log(game))); // ✅ Log the games to the console
  }
  removeCart() {
    this.store.dispatch(CartActions.removeCart());
  }


}
