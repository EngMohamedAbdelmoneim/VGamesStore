import { SearchService } from './../../../core/services/search.service';
import {applyingFilter, loadFilteredGamesFailure, loadFilteredGamesSuccess, searchGames, searchGamesFailure, searchGamesSuccess } from "./search.actions";
import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, switchMap } from "rxjs";

@Injectable()
export class SearchEffects {
  private actions$ = inject(Actions);
  private searchService = inject(SearchService);

searchGames$ = createEffect(() =>
  this.actions$.pipe(
    ofType(searchGames),
    mergeMap((action) =>
      this.searchService.searchGames(action.searchKeyWord).pipe(
        map((searchedgames) => searchGamesSuccess({ searchedgames })),
        catchError((error) => of(searchGamesFailure({ error: error.message })))
      )
    )
  )
);

// for filter games
applyFilter$ = createEffect(() =>
  this.actions$.pipe(
    ofType(applyingFilter),
    switchMap(({ filter }) =>
      this.searchService.filterGames(filter).pipe(
        map((searchedgames) => searchGamesSuccess({ searchedgames })),
        catchError((error) => of(searchGamesFailure({ error: error.message })))
      )
    )
  )
);
}
