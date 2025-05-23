import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { loadGames, loadGamesSuccess, loadGamesFailure, loadGameDetails, loadGameDetailsSuccess, loadGameDetailsFailure } from './game.actions';
import { GameService } from '../../../core/services/game.service';

@Injectable()
export class GameEffects {
  private actions$ = inject(Actions);
  private gameService = inject(GameService);

  loadGames$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadGames),
      mergeMap(() =>
        this.gameService.getGames().pipe(
          map((games) => loadGamesSuccess({ games })),
          catchError((error) => of(loadGamesFailure({ error: error.message })))
        )
      )
    )
  );


  loadGameDetails$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadGameDetails),
      mergeMap((action) =>
        this.gameService.getGameById(action.id).pipe(
          map((game) => loadGameDetailsSuccess({ game })),
          catchError(error => of(loadGameDetailsFailure({ error: error.message })))
        )
      )
    )
  );
}
