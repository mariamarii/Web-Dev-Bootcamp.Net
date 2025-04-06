import { Movie } from "./Movie";
export interface IMovieAPI {
    searchMovies(query: string): Promise<Movie[]>;
  }
  