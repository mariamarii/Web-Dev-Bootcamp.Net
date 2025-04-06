import { Movie } from "./Movie.js";


export interface IMovieElements {
  updateFeatured(movie: Movie): void;
  renderMovieCards(movies: Movie[], onCardFocus: (movie: Movie) => void): void;
}
