import { createAction, props } from '@ngrx/store';
import { AuthResponse } from '../../../core/models/auth';


export const login = createAction(
  '[Auth] Login',
  props<{ email: string; password: string }>()
);

export const loginSuccess = createAction(
  '[Auth] Login Success',
  props<{ user: AuthResponse }>()
);

export const loginFailure = createAction(
  '[Auth] Login Failure',
  props<{ error: string }>()
);

export const logout = createAction('[Auth] Logout');


// Token Refresh Actions
export const refresh = createAction('[Auth] Refresh Token');

export const refreshSuccess = createAction(
  '[Auth] Refresh Token Success',
  props<{ user: AuthResponse }>()
);

export const refreshFailure = createAction('[Auth] Refresh Token Failure');


