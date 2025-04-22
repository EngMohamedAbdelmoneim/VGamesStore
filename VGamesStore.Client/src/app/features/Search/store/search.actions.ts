import { createAction, props } from "@ngrx/store";
import { Game } from "../../../core/models/game";
import { Filter } from "../../../core/models/filter";

export const searchGames = createAction('[Search] Search Games', props<{ searchKeyWord: string }>());
export const searchGamesSuccess = createAction('[Search] Search Games Success', props<{ searchedgames: Game[] }>());
export const searchGamesFailure = createAction('[Search] Search Games Failure', props<{ error: string }>());


// Filter Actions
export const applyingFilter = createAction(
  '[Search] Apply Filter',
  props<{ filter: Filter }>()
);

export const loadFilteredGamesSuccess = createAction(
  '[Search] Load Filtered Games Success',
  props<{ games: Game[] }>()
);

export const loadFilteredGamesFailure = createAction(
  '[Search] Load Filtered Games Failure',
  props<{ error: string }>()
);
