var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { Movie } from "./types/Movie";
const testMovie = new Movie("Title", "Overview", 7.5, "2024-01-01", "/path.jpg", "/poster.jpg");
console.log(testMovie.posterUrl);
class MovieAPI {
    constructor() {
        this.apiKey = '21d6601622ce880a80939f3c1823ce8e';
        this.baseUrl = 'https://api.themoviedb.org/3';
    }
    searchMovies(query) {
        return __awaiter(this, void 0, void 0, function* () {
            const res = yield fetch(`${this.baseUrl}/search/movie?api_key=${this.apiKey}&query=${query}`);
            const data = yield res.json();
            return data.results.map((m) => new Movie(m.title, m.overview, m.vote_average, m.release_date, m.backdrop_path, m.poster_path));
        });
    }
}
class MovieUI {
    updateFeatured(movie) {
        document.getElementById("rating").textContent = movie.rating.toFixed(1);
        document.getElementById("year").textContent = movie.year;
        document.getElementById("description").textContent = movie.overview;
        const containerSection = document.querySelector(".container");
        containerSection.style.backgroundImage = `url(${movie.backdropUrl})`;
        const title = document.querySelector(".hero .title");
        title.textContent = movie.title;
    }
    renderMovieCards(movies, onCardFocus) {
        const container = document.getElementById("movie-slider");
        container.innerHTML = "";
        movies.forEach((movie, index) => {
            const card = document.createElement("div");
            card.className = "movie-card";
            card.setAttribute("data-index", index.toString());
            card.innerHTML = `
          <img src="${movie.posterUrl}" alt="${movie.title}" />
          
        `;
            container.appendChild(card);
        });
        this.addSliderEvents(container, movies, onCardFocus);
    }
    addSliderEvents(container, movies, onCardFocus) {
        const prevBtn = document.querySelector(".slider-btn.prev");
        const nextBtn = document.querySelector(".slider-btn.next");
        const cards = Array.from(container.querySelectorAll(".movie-card"));
        let currentIndex = 0;
        const updateSlider = () => {
            cards.forEach(card => card.classList.remove("active"));
            const targetCard = cards[currentIndex];
            targetCard.classList.add("active");
            targetCard.scrollIntoView({ behavior: "smooth", inline: "center", block: "nearest" });
            onCardFocus(movies[currentIndex]);
        };
        prevBtn.onclick = () => {
            if (currentIndex > 0) {
                currentIndex--;
                updateSlider();
            }
        };
        nextBtn.onclick = () => {
            if (currentIndex < movies.length - 1) {
                currentIndex++;
                updateSlider();
            }
        };
        // Initial display
        updateSlider();
    }
}
class MovieApp {
    constructor() {
        this.api = new MovieAPI();
        this.ui = new MovieUI();
    }
    init() {
        return __awaiter(this, void 0, void 0, function* () {
            const movies = yield this.api.searchMovies("spiderman");
            if (movies.length) {
                this.ui.updateFeatured(movies[0]);
                this.ui.renderMovieCards(movies, (movie) => {
                    this.ui.updateFeatured(movie);
                });
            }
        });
    }
}
const app = new MovieApp();
app.init();
