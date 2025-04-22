import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = 'https://localhost:7229/api/Game'; // Replace with actual API

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl+'/GetAllGames'); // ✅ Ensure the correct endpoint is used
  }

  getGameById(id: number): Observable<Game> {
    console.log('Fetching game with ID:', id); // ✅ Log the game ID to the console
    return this.http.get<Game>(`${this.apiUrl}/GetGameById/${id}`);
  }
}

