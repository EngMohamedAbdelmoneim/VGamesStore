import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import {jwtDecode} from 'jwt-decode';
import { inject, Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { refresh } from '../features/auth/store/auth.actions';
import { AuthService } from '../core/services/auth.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor  implements HttpInterceptor {

  constructor(private authService : AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = this.authService.getToken();

    let authRequest = req;

    if (token) {
      authRequest = req.clone({
        setHeaders:  { Authorization: `Bearer ${token}` }
      });
  }

return next.handle(authRequest).pipe(
      catchError((error: HttpErrorResponse) => {

        // ✅ If Unauthorized, try Refresh Token
        if (error.status === 401 && this.authService.getRefreshToken()) {
          return this.authService.refreshToken().pipe(
            switchMap((newToken: string) => {

              this.authService.setToken(newToken);

              const retryRequest = req.clone({
                setHeaders: { Authorization: `Bearer ${newToken}` }
              });

              return next.handle(retryRequest);
            }),
            catchError(() => {
              this.authService.logout();
              return throwError(() => error);
            })
          );
        }

        return throwError(() => error);
      })
    );  }

}
