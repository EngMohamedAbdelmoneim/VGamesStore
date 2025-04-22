import { Component, inject, Input } from '@angular/core';
import { Filter } from '../../../../core/models/filter';
import { applyingFilter } from '../../store/search.actions';
import { Store } from '@ngrx/store';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter',
  imports: [FormsModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent {
  private store = inject(Store);
  @Input() filter: Filter = {
    keyword: ' ',
    categoryId: null,
    minPrice: null,
    maxPrice: null,
    developer: null,
    sortBy: 'title',
    ascending: true
  };
  onFilterChange(updatedFilters: Filter) {
    this.store.dispatch(applyingFilter({ filter: updatedFilters }));
  }
  reset() {
    this.filter = {
      keyword: ' ',
      categoryId: null,
      minPrice: null,
      maxPrice: null,
      developer: null,
      sortBy: 'title',
      ascending: true
    };
  }
}
