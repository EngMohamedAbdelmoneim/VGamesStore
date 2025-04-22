import { Category } from './../models/category';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:7229/api/Category'; // Replace with actual API

  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(this.apiUrl + '/GetAllCategories'); // ✅ Ensure the correct endpoint is used
  }
  getCategoryById(id: number) : Observable<Category>{
    console.log('Fetching category with ID:', id); // ✅ Log the category ID to the console
    return this.http.get<Category>(`${this.apiUrl}/GetCategoryById/${id}`);
  }

}
