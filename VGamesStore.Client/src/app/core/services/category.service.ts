import { Genre } from '../models/genre';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:7229/api/Category'; // Replace with actual API

  constructor(private http: HttpClient) {}

  getCategories(): Observable<Genre[]>{
    return this.http.get<Genre[]>(this.apiUrl + '/GetAllCategory'); // ✅ Ensure the correct endpoint is used
  }
  getCategoryById(id: number) : Observable<Genre>{
    console.log('Fetching category with ID:', id); // ✅ Log the category ID to the console
    return this.http.get<Genre>(`${this.apiUrl}/GetCategoryById/${id}`);
  }

}
