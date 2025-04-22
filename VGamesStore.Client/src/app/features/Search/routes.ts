import { SearchGamesListPageComponent } from './pages/search-games-list-page/search-games-list-page.component';
import { Routes } from "@angular/router";

export const Search_ROUTES: Routes = [
  { path: ':keyword', component: SearchGamesListPageComponent },
];
