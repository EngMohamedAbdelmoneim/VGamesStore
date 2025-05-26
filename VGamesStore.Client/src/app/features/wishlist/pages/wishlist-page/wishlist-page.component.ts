import { removeWishlist } from './../../store/wishlist.actions';
import { WishlistItem } from './../../../../core/models/wishlist-item';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectAllwishlistItems, selectError, selectLoading } from '../../store/wishlist.selectors';
import * as WishlistActions from '../../store/wishlist.actions';
import { CommonModule } from '@angular/common';
import { WishlistItemCardComponent } from "../../components/wishlist-item-card/wishlist-item-card.component";
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-wishlist-page',
  imports: [CommonModule, WishlistItemCardComponent, MatIconModule],
  templateUrl: './wishlist-page.component.html',
  styleUrl: './wishlist-page.component.css'
})
export class WishlistPageComponent implements OnInit {
  private store = inject(Store);

  textArray = 'Your Wishlist'.split('');
  arrows = Array(3); // Three arrows

  WishlistItems$: Observable<WishlistItem[]> = this.store.select(selectAllwishlistItems);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);

  ngOnInit(): void {
    this.store.dispatch(WishlistActions.loadWishlistItems());
    console.log(this.WishlistItems$.forEach((game) => console.log(game))); // ✅ Log the games to the console
  }
  removeWishlist() {
    this.store.dispatch(WishlistActions.removeWishlist());
  }


}
