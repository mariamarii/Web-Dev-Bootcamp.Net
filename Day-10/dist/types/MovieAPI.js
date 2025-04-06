import { Movie } from "./Movie.js";
export class MovieAPI {
    constructor() {
        this.apiKey = '21d6601622ce880a80939f3c1823ce8e';
        this.baseUrl = 'https://api.themoviedb.org/3';
    }
    searchMovies(query) {
        return fetch(`${this.baseUrl}/search/movie?api_key=${this.apiKey}&query=${query}`)
            .then(res => res.json())
            .then(data => {
            return data.results.map((m) => new Movie(m.title, m.overview, m.vote_average, m.release_date, m.backdrop_path, m.poster_path, m.vote_count));
        })
            .catch(error => {
            console.error('Error fetching movies:', error);
            throw error;
        });
    }
}
