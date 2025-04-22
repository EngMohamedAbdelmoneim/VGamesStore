export interface Filter {
  keyword : string,
  categoryId : number | null,
  minPrice : number | null,
  maxPrice : number | null,
  developer : string | null,
  sortBy : string | "title"; // or "releaseDate", "pric
  ascending : boolean | true;
}
