
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../models/genre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private apiUrl = 'https://localhost:7229/api/Genre'; // Replace with actual API

  constructor(private http: HttpClient) {}

  getGenres(): Observable<Genre[]>{
    return this.http.get<Genre[]>(this.apiUrl + '/GetAllGenre'); // ✅ Ensure the correct endpoint is used
  }
  getGenreById(id: number) : Observable<Genre>{
    console.log('Fetching category with ID:', id); // ✅ Log the category ID to the console
    return this.http.get<Genre>(`${this.apiUrl}/GetGenreById/${id}`);
  }

}
