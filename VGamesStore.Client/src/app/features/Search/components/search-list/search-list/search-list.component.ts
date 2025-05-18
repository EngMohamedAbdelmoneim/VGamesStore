import { distinctUntilChanged, map, Observable } from 'rxjs';
import { Component, inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import * as SearchActions from '../../../store/search.actions';
import { ActivatedRoute } from '@angular/router';
import { Game } from '../../../../../core/models/game';
import { GameCardComponent } from '../../../../games/components/game.card/game.card.component';
import { selectSearchedGames, selectSearchOrigin } from '../../../store/search.selectors';
import { selectError, selectLoading } from '../../../../games/store/game.selectors';

@Component({
  selector: 'app-search-list',
  imports: [CommonModule, GameCardComponent],
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css']
})
export class SearchListComponent implements OnInit {
  private store = inject(Store);
  private route = inject(ActivatedRoute);

  searchGames$: Observable<Game[]> = this.store.select(selectSearchedGames);
  loading$: Observable<boolean> = this.store.select(selectLoading);
  error$: Observable<string | null> = this.store.select(selectError);
  origin$: Observable<string | null> = this.store.select(selectSearchOrigin);

  ngOnInit(): void {
    this.route.url
      .pipe(
        map(segments => {
          const urlPath = segments.map(segment => segment.path).join('/');
          return urlPath;
        }),
        distinctUntilChanged()
      )
      .subscribe(urlPath => {
        // Check if 'genre', 'filter', or 'normal' exists in the URL path
        if (urlPath.includes('genre')) {
          // Handle genre search
          const genre = this.getSegmentValue(urlPath, 'genre');
          if (genre) {
            console.log('Search Genre Changed:', genre);
            this.store.dispatch(SearchActions.searchGamesByGenresName({ genreName: genre }));
          }
        } else if (urlPath.includes('filter')) {
          // Handle filter search
          // const filter = this.getSegmentValue(urlPath, 'filter');
            this.route.queryParams.subscribe(params => {
              const filter = {
                keyword: params['keyword'] === 'null' ? null : params['keyword'],
                genreName: params['genreName'] ? params['genreName'] : null,
                minPrice: params['minPrice'] ? +params['minPrice'] : null,
                maxPrice: params['maxPrice'] ? +params['maxPrice'] : null,
                developer: params['developer'] === 'null' ? null : params['developer'],
                sortBy: params['sortBy'],
                ascending: params['ascending'] === 'true'
              };

              console.log('Received filter:', filter);

              // Dispatch your NgRx action here
              this.store.dispatch(SearchActions.applyingFilterDto({ filter: filter }));
            });

        } else if (urlPath.includes('normal')) {
          // Handle normal keyword search
          const keyword = this.getSegmentValue(urlPath, 'normal');
          if (keyword) {
            console.log('Search Keyword Changed:', keyword);
            this.store.dispatch(SearchActions.searchGames({ searchKeyWord: keyword }));
          }
        }
      });
  }

  // Helper function to extract value from the URL path based on segment name
  private getSegmentValue(urlPath: string, segment: string): string | null {
    const segments = urlPath.split('/');
    const index = segments.indexOf(segment);
    return index !== -1 && segments[index + 1] ? segments[index + 1] : null;
  }
}
