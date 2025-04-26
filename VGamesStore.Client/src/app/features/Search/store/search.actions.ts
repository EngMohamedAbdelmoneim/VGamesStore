import { createAction, props } from "@ngrx/store";
import { Game } from "../../../core/models/game";
import { FilterDto } from "../../../core/models/filter-dto";

export const searchGames = createAction('[Search] Search Games', props<{ searchKeyWord: string }>());
export const searchGamesSuccess = createAction('[Search] Search Games Success', props<{ searchedgames: Game[] }>());
export const searchGamesFailure = createAction('[Search] Search Games Failure', props<{ error: string }>());


// FilterDto Actions
export const applyingFilterDto = createAction(
  '[Search] Apply FilterDto',
  props<{ filter: FilterDto }>()
);

export const loadFilterDtoedGamesSuccess = createAction(
  '[Search] Load FilterDtoed Games Success',
  props<{ games: Game[] }>()
);

export const loadFilterDtoedGamesFailure = createAction(
  '[Search] Load FilterDtoed Games Failure',
  props<{ error: string }>()
);
