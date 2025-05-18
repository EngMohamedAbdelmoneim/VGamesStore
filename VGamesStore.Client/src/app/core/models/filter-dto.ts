export interface FilterDto {
  keyword : string | null,
  genreName : string | null,
  minPrice : number | null,
  maxPrice : number | null,
  developer : string | null,
  sortBy : string | "title"; // or "releaseDate", "pric
  ascending : boolean | true;
}
