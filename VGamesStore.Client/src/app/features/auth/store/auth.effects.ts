import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
 import { login, loginSuccess, loginFailure, refresh, refreshFailure, refreshSuccess } from './auth.actions';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';



@Injectable()
export class AuthEffects {

  constructor(
    private actions$: Actions,
    private authService: AuthService
  ) {}

  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(login),
      mergeMap(action =>
        this.authService.login(action.email, action.password).pipe(
          map(user => loginSuccess({ user })),
          catchError(() => of(loginFailure({ error: 'Invalid credentials' })))
        )
      )
    )
  );


  saveUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginSuccess),
      tap(({ user }) => {
        localStorage.setItem('token', user.token);
        localStorage.setItem('refreshToken', user.refreshToken);
        localStorage.setItem('user', JSON.stringify(user));
      })
    ),
    { dispatch: false }
  );

}
