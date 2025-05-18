import { createAction, props } from "@ngrx/store";
import { Game } from "../../../core/models/game";
import { FilterDto } from "../../../core/models/filter-dto";

export const searchGames = createAction(
  '[Search] Search Games',
  props<{ searchKeyWord: string }>()
);

export const searchGamesByGenresName = createAction(
  '[Search] Search Games By Genre',
  props<{ genreName: string }>()
);

export const applyingFilterDto = createAction(
  '[Search] Apply FilterDto',
  props<{ filter: FilterDto }>()
);

export const searchGamesSuccess = createAction(
  '[Search] Search Games Success',
  props<{ searchedgames: Game[], origin: 'keyword' | 'genre' | 'filter' }>()
);

export const searchGamesFailure = createAction(
  '[Search] Search Games Failure',
  props<{ error: string }>()
);
