import { Genre } from '../../../../core/models/genre';
import { Component, inject, Input, OnInit } from '@angular/core';
import { FilterDto } from '../../../../core/models/filter-dto';
import { applyingFilterDto } from '../../store/search.actions';
import { Store } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../../../core/services/category.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-filter',
  imports: [FormsModule,CommonModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterDtoComponent implements OnInit {

  filter: FilterDto = {
    keyword: null,
    categoryId: null,
    minPrice: null,
    maxPrice: null,
    developer: null,
    sortBy: 'title',
    ascending: true
  };
  private store = inject(Store);
  private CategoryService = inject(CategoryService);

  Categories: Genre[] | null = null;

  ngOnInit(): void {
    this.CategoryService.getCategories().subscribe({
      next: (categories) => {
        this.Categories = categories;
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    });
  }

  onFilterDtoChange(updatedFilterDtos: FilterDto) {
    this.store.dispatch(applyingFilterDto({ filter: updatedFilterDtos }));
    this.reset()
  }
  reset() {
    this.filter = {
      keyword: null,
      categoryId: null,
      minPrice: null,
      maxPrice: null,
      developer: null,
      sortBy: 'title',
      ascending: true
    };
  }
}
