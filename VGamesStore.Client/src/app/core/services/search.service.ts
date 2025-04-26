import { FilterDto } from '../models/filter-dto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Game } from '../models/game';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiUrl = 'https://localhost:7229/api/search';
  constructor(private http: HttpClient) { }

  searchGames(searchKeyWord: string) {
    return this.http.get<Game[]>(`${this.apiUrl}/SearchGames/search?Keyword=${searchKeyWord}`);
  }

  filterGames(query : FilterDto) {
    let params = new HttpParams();

    for (let key in query) {
      const value = (query as any)[key];
      if (value !== null && value !== undefined && value !== '') {
        params = params.append(key, value.toString());
      }
    }
    return this.http.get<Game[]>(`${this.apiUrl}/FilterGames/search`, {params});
  }
}


