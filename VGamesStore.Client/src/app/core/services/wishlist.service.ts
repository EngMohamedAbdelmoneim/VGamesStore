import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';
import { GuidService } from './guid.service';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'https://localhost:7229/api/Wishlist';
  constructor(private http : HttpClient,private guidService : GuidService) { }
}
