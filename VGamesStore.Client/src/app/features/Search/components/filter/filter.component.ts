import { Genre } from '../../../../core/models/genre';
import { Component, inject, Input, OnInit } from '@angular/core';
import { FilterDto } from '../../../../core/models/filter-dto';
import { applyingFilterDto } from '../../store/search.actions';
import { Store } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { GenreService } from '../../../../core/services/genre.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-filter',
  imports: [FormsModule, CommonModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterDtoComponent implements OnInit {

  filter: FilterDto = {
    keyword: null,
    genreName: null,
    minPrice: null,
    maxPrice: null,
    developer: null,
    sortBy: 'title',
    ascending: true
  };
  private store = inject(Store);
  private router = inject(Router);
  private CategoryService = inject(GenreService);

  Categories: Genre[] | null = null;

  ngOnInit(): void {
    this.CategoryService.getGenres().subscribe({
      next: (categories) => {
        this.Categories = categories;
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    });
  }

  onFilterDtoChange(updatedFilterDtos: FilterDto) {
    this.router.navigate(['/search/filter'], {
      queryParams: updatedFilterDtos
    });
  }
  reset() {
    this.filter = {
      keyword: null,
      genreName: null,
      minPrice: null,
      maxPrice: null,
      developer: null,
      sortBy: 'title',
      ascending: true
    };
  }
}
