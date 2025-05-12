import { Genre } from "./genre";

export interface Game {
    id: number;
    title: string;
    description: string;
    price: number;
    type: string;
    developer: string;
    releaseDate: Date;
    imageUrl?: string | null;
    imagesUrls?: string[] | null;
    genres?:Genre[] | null;
  }
