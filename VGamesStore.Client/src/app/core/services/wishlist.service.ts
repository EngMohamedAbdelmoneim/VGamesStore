import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { GuidService } from './guid.service';
import { Observable } from 'rxjs';
import { Wishlist } from '../models/wishlist';
import { WishlistItem } from '../models/wishlist-item';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'https://localhost:7229/api/Wishlist';
  constructor(private http : HttpClient,private guidService : GuidService) { }
  createAndGetWishlist(): Observable<Wishlist> {
      const wishlistId = this.guidService.getOrCreate('wishlistId');
      console.log(`Game with ID ${wishlistId} added to wishlist.`);
      return this.http.get<Wishlist>(this.apiUrl + `/GetWishlist/${"wishlist-"+wishlistId}`);
    }
    getWishlist(): Observable<Wishlist> {
      const wishlistId = this.guidService.getOrCreate('wishlistId');
      console.log(`Game with ID ${"wishlist/"+wishlistId} added to wishlist.`);
      return this.http.get<Wishlist>(this.apiUrl + `/GetWishlist/${"wishlist-"+wishlistId}`);
    }
    removeWishlist(): Observable<any> {
      const wishlistId = this.guidService.get('wishlistId');
      return this.http.delete<any>(this.apiUrl + `/DeleteWishlist/${"wishlist-"+wishlistId}`);
    }
    addOrUpdateWishlistItem( wishlistItem: WishlistItem): Observable<any> {
      console.log(`Game with ID ${wishlistItem.gameName} added to wishlist.`);
      const wishlistId = this.guidService.getOrCreate('wishlistId');
      return this.http.post(this.apiUrl + `/AddOrUpdateItem/${"wishlist-"+wishlistId}`, wishlistItem);
    }
    RemoveWishlistItem( wishlistItemId: number): Observable<Wishlist> {
      console.log(`Game with ID ${wishlistItemId} added to wishlist.`);
      const wishlistId = this.guidService.getOrCreate('wishlistId');
      return this.http.delete<Wishlist>(this.apiUrl + `/RemoveItem/${"wishlist-"+wishlistId}/remove/${wishlistItemId}`);
    }
}
