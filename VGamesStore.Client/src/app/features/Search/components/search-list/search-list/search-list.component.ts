import { Observable } from 'rxjs';
import { Component, inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import * as SearchActions from '../../../store/search.actions';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Game } from '../../../../../core/models/game';
import { selectError, selectLoading, selectSearchedGames } from '../../../store/search.selectors';
import { GameCardComponent } from '../../../../games/components/game.card/game.card.component';

@Component({
  selector: 'app-search-list',
  imports: [CommonModule, RouterLink, GameCardComponent],
  templateUrl: './search-list.component.html',
  styleUrl: './search-list.component.css'
})
export class SearchListComponent implements OnInit {
  private store = inject(Store);
  private route = inject(ActivatedRoute);

  searchGames$: Observable<Game[]> = this.store.select(selectSearchedGames);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);

  ngOnInit(): void {
    const searchKeyWord = this.route.snapshot.paramMap.get('keyword');
    console.log('Search KeyWord:', searchKeyWord);
    if (searchKeyWord) {
      this.store.dispatch(SearchActions.searchGames({ searchKeyWord: searchKeyWord }));
      console.log('Searched Games Load', this.searchGames$);
    } else {
      console.error('Invalid game ID:', searchKeyWord);
    }
  }
}
