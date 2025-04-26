import { Component, inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as GameActions from '../../store/game.actions';
import { Observable } from 'rxjs';
import { selectAllGames, selectLoading, selectError } from '../../store/game.selectors';
import { Game } from '../../../../core/models/game';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { imagePath } from '../../../../utils/imagePath';
import { GameCardComponent } from "../../components/game.card/game.card.component";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-game-list',
  imports: [CommonModule, GameCardComponent,FormsModule],
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {
    private store = inject(Store);
    private router = inject(Router);
    searchText: string = '';
    textArray = 'GAMES LIST'.split('');
    arrows = Array(3); // Three arrows

  // ✅ Inject the store using inject() function
  games$: Observable<Game[]> = this.store.select(selectAllGames);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);

  ngOnInit(): void {
    this.store.dispatch(GameActions.loadGames());
    console.log(this.games$.forEach((game) => console.log(game))); // ✅ Log the games to the console
    // ✅ Dispatch action to load games
  }
  getFormattedPrice(Url:any | null): any {
    return imagePath(Url);
  }
  goToSearch() {
    if (this.searchText && this.searchText.trim()) {
      this.router.navigate(['/search', this.searchText]);
    }
  }
}
