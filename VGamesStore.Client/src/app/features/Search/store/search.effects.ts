import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, switchMap } from 'rxjs';
import { SearchService } from '../../../core/services/search.service';
import * as SearchActions from './search.actions';

@Injectable()
export class SearchEffects {
  private actions$ = inject(Actions);
  private searchService = inject(SearchService);

  searchGames$ = createEffect(() =>
    this.actions$.pipe(
      ofType(SearchActions.searchGames),
      mergeMap((action) =>
        this.searchService.searchGames(action.searchKeyWord).pipe(
          map((searchedgames) => SearchActions.searchGamesSuccess({ searchedgames, origin: 'keyword' })),
          catchError((error) => of(SearchActions.searchGamesFailure({ error: error.message })))
        )
      )
    )
  );

  searchGamesByGenreName$ = createEffect(() =>
    this.actions$.pipe(
      ofType(SearchActions.searchGamesByGenresName),
      mergeMap((action) =>
        this.searchService.searchGamesByGenresName(action.genreName).pipe(
          map((searchedgames) => SearchActions.searchGamesSuccess({ searchedgames, origin: 'genre' })),
          catchError((error) => of(SearchActions.searchGamesFailure({ error: error.message })))
        )
      )
    )
  );

  applyFilterDto$ = createEffect(() =>
    this.actions$.pipe(
      ofType(SearchActions.applyingFilterDto),
      switchMap(({ filter }) =>
        this.searchService.filterGames(filter).pipe(
          map((searchedgames) => SearchActions.searchGamesSuccess({ searchedgames, origin: 'filter' })),
          catchError((error) => of(SearchActions.searchGamesFailure({ error: error.message })))
        )
      )
    )
  );
}
