import { MovieAPI } from "./MovieAPI.js";
import { MovieElements } from "./MovieElements.js"; 
import { Movie } from "./Movie.js";
import { IApp } from "./IApp.js";

export class App implements IApp {
  private api: MovieAPI;
  private elements: MovieElements;
  private allMovies: Movie[] = [];  

  constructor() {
    this.api = new MovieAPI();
    this.elements = new MovieElements();
  }

  async init(): Promise<void> {
    await this.loadSpiderMovies();
    this.setupSearch();
  }

  private async loadSpiderMovies(): Promise<void> {
    const movies = await this.api.searchMovies("");

    if (movies.length) {
      this.allMovies = movies;
      this.elements.updateFeatured(movies[0]);
      this.elements.renderMovieCards(movies, (movie: Movie) => {
        this.elements.updateFeatured(movie);
      });
    } else {
      console.log("No movies found.");
    }
  }

  private setupSearch(): void {
    const searchInput = document.getElementById('search-input') as HTMLInputElement;

    if (searchInput) {
      searchInput.addEventListener('input', async (event) => { 
        const inputElement = event.target as HTMLInputElement;
        const query = inputElement.value.trim().toLowerCase();

        if (query.length > 0) {
          const searchMovies = await this.api.searchMovies(query); 
          this.renderMovies(searchMovies);
        } else {
          this.renderMovies(this.allMovies);
        }
      });
    }
  }

  

  private renderMovies(movies: Movie[]): void {
    this.elements.renderMovieCards(movies, (movie: Movie) => {
      this.elements.updateFeatured(movie);
    });
  }
}