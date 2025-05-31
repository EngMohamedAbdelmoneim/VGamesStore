import { Subscription } from 'rxjs';
import { CartService } from './core/services/cart.service';
import { GuidService } from './core/services/guid.service';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'VGamesStore.Client';
  constructor(guidService : GuidService,store: Store) {
    guidService.getOrCreate('cartId');
    guidService.getOrCreate('wishlistId');
  }
}
