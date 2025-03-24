import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideStore, provideState } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { importProvidersFrom } from '@angular/core';

import { provideHttpClient } from '@angular/common/http'; // ✅ Ensure this is imported
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';
import { gameReducer } from './app/features/games/store/game.reducer';
import { GameEffects } from './app/features/games/store/game.effects';


bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes, withComponentInputBinding()),
    provideStore(),
    provideStore({ games: gameReducer }),
    provideEffects([GameEffects]), // ✅ Provide state with correct feature key    provideEffects(GameEffects), // ✅ Register effects CORRECTLY (remove extra brackets)
    provideHttpClient(),
  ]
}).catch(err => console.error(err));

