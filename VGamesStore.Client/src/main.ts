import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideStore, provideState } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';

import { provideHttpClient } from '@angular/common/http'; // ✅ Ensure this is imported
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';
import { gameReducer, gamesReducer } from './app/features/games/store/game.reducer';
import { GameEffects } from './app/features/games/store/game.effects';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes, withComponentInputBinding()),
    provideStore(),
    provideStore({ games: gamesReducer,game : gameReducer }),
    provideEffects([GameEffects]), // ✅ Provide state with correct feature key    provideEffects(GameEffects), // ✅ Register effects CORRECTLY (remove extra brackets)
    provideHttpClient(),
  ]
}).catch(err => console.error(err));

