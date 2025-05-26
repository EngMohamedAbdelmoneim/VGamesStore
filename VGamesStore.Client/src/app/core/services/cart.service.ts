import { CartItem } from './../models/cart-item';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GuidService } from './guid.service';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'https://localhost:7229/api/Cart';
  constructor(private http : HttpClient,private guidService : GuidService) { }

  createAndGetCart(): Observable<Cart> {
    const cartId = this.guidService.getOrCreate('cartId');
    console.log(`Game with ID ${cartId} added to cart.`);
    return this.http.get<Cart>(this.apiUrl + `/GetCart/${"cart-"+cartId}`);
  }
  getCart(): Observable<Cart> {
    const cartId = this.guidService.getOrCreate('cartId');
    console.log(`Game with ID ${"cart/"+cartId} added to cart.`);
    return this.http.get<Cart>(this.apiUrl + `/GetCart/${"cart-"+cartId}`);
  }
  removeCart(): Observable<any> {
    const cartId = this.guidService.get('cartId');
    return this.http.delete<any>(this.apiUrl + `/DeleteCart/${"cart-"+cartId}`);
  }
  addOrUpdateCartItem( cartItem: CartItem): Observable<any> {
    console.log(`Game with ID ${cartItem.gameName} added to cart.`);
    const cartId = this.guidService.getOrCreate('cartId');
    return this.http.post(this.apiUrl + `/AddOrUpdateItem/${"cart-"+cartId}`, cartItem);
  }
  RemoveCartItem( cartItemId: number): Observable<Cart> {
    console.log(`Game with ID ${cartItemId} added to cart.`);
    const cartId = this.guidService.getOrCreate('cartId');
    return this.http.delete<Cart>(this.apiUrl + `/RemoveItem/${"cart-"+cartId}/remove/${cartItemId}`);
  }

}
