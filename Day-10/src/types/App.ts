import { MovieAPI } from "./MovieAPI.js";
import { MovieElements } from "./MovieElements.js"; 
import { Movie } from "./Movie.js";
import { IApp } from "./IApp.js";

export class App implements IApp {
  private api: MovieAPI;
  private elements: MovieElements;
  private allSpiderMovies: Movie[] = [];  

  constructor() {
    this.api = new MovieAPI();
    this.elements = new MovieElements();
  }

  async init(): Promise<void> {
    await this.loadSpiderMovies();

    this.setupSearch();
  }

  private async loadSpiderMovies(): Promise<void> {
    const movies = await this.api.searchMovies("spiderman");

    if (movies.length) {
      this.allSpiderMovies = movies;

      this.elements.updateFeatured(movies[0]);

      this.elements.renderMovieCards(movies, (movie: Movie) => {
        this.elements.updateFeatured(movie);
      });
    } else {
      console.log("No spider-related movies found.");
    }
  }

  private setupSearch(): void {
    const searchInput = document.getElementById('search-input') as HTMLInputElement;

    if (searchInput) {
      searchInput.addEventListener('input', (event) => {
        const inputElement = event.target as HTMLInputElement;
        const query = inputElement.value.trim().toLowerCase();

        if (query.length > 0) {
          this.filterMovies(query);
        } else {
          this.renderMovies(this.allSpiderMovies);
        }
      });
    }
  }

  private filterMovies(query: string): void {
    const filteredMovies = this.allSpiderMovies.filter(movie => {
      return movie.title.toLowerCase().includes(query); 
    });

   
    this.renderMovies(filteredMovies);
  }

  private renderMovies(movies: Movie[]): void {
    this.elements.renderMovieCards(movies, (movie: Movie) => {
      this.elements.updateFeatured(movie);
    });
  }
}
