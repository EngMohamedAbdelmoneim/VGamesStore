import { selectGameState } from '../../store/game.selectors';
import { Component, inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as GameActions from '../../store/game.actions';
import { Observable } from 'rxjs';
import { selectAllGames, selectLoading, selectError } from '../../store/game.selectors';
import { Game } from '../../../../core/models/game';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-game-list',
  imports: [CommonModule],
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {
  private store = inject(Store);

  games$: Observable<Game[]> = this.store.select(selectAllGames);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);

  ngOnInit(): void {
    this.games$.forEach(games => {  // ✅ Subscribe to games observable
      console.log(games); // ✅ Log games to console
    });
    this.store.dispatch(GameActions.loadGames()); // ✅ Dispatch action to load games
  }
  imagePath(imageUrl : any | null): string | null {
    const baseUrl = 'https://localhost:7229'; // Change to match your API URL
    return imageUrl ? `${baseUrl}${imageUrl}` : null;
  }
}
