var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { MovieAPI } from "./MovieAPI.js";
import { MovieElements } from "./MovieElements.js";
export class App {
    constructor() {
        this.allMovies = [];
        this.api = new MovieAPI();
        this.elements = new MovieElements();
    }
    init() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.loadSpiderMovies();
            this.setupSearch();
        });
    }
    loadSpiderMovies() {
        return __awaiter(this, void 0, void 0, function* () {
            const movies = yield this.api.searchMovies("");
            if (movies.length) {
                this.allMovies = movies;
                this.elements.updateFeatured(movies[0]);
                this.elements.renderMovieCards(movies, (movie) => {
                    this.elements.updateFeatured(movie);
                });
            }
            else {
                console.log("No movies found.");
            }
        });
    }
    setupSearch() {
        const searchInput = document.getElementById('search-input');
        if (searchInput) {
            searchInput.addEventListener('input', (event) => __awaiter(this, void 0, void 0, function* () {
                const inputElement = event.target;
                const query = inputElement.value.trim().toLowerCase();
                if (query.length > 0) {
                    const searchMovies = yield this.api.searchMovies(query);
                    this.renderMovies(searchMovies);
                }
                else {
                    this.renderMovies(this.allMovies);
                }
            }));
        }
    }
    renderMovies(movies) {
        this.elements.renderMovieCards(movies, (movie) => {
            this.elements.updateFeatured(movie);
        });
    }
}
