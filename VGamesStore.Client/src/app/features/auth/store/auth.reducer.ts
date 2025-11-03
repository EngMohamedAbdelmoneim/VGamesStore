import { createReducer, on } from '@ngrx/store';
import { loginSuccess, logout, refreshFailure, refreshSuccess } from './auth.actions';
import { AuthResponse } from '../../../core/models/auth';

export interface AuthState {
  user: AuthResponse | null;
}

export const initialState: AuthState = {
  user: null
};

export const authReducer = createReducer(
  initialState,
  on(loginSuccess, (state, { user }) => {
    localStorage.setItem('token', user.token);
    localStorage.setItem('refreshToken', user.refreshToken);
    return { ...state, user };
  }),
  on(logout, (state) => {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    return { ...state, user: null };
  }),

  on(refreshSuccess, (state, { user }) => {
  localStorage.setItem('token', user.token);
  localStorage.setItem('refreshToken', user.refreshToken);
  return { ...state, user };
}),
on(refreshFailure, state => {
  localStorage.removeItem('token');
  localStorage.removeItem('refreshToken');
  return { ...state, user: null };
})

);
