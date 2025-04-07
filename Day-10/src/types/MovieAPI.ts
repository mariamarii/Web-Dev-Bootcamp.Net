import { IMovieAPI } from "./IMovieAPI.js";
import { Movie } from "./Movie.js";

export class MovieAPI implements IMovieAPI {
    private apiKey: string = '21d6601622ce880a80939f3c1823ce8e';
    private baseUrl: string = 'https://api.themoviedb.org/3';
  
    searchMovies(query: string): Promise<Movie[]> {
      if(query){
        return fetch(`${this.baseUrl}/search/movie?api_key=${this.apiKey}&query=${query}`)
        .then(res => res.json())
        .then(data => {
          return data.results.map((m: any) => new Movie(
            m.title,
            m.overview,
            m.vote_average,
            m.release_date,
            m.backdrop_path,
            m.poster_path,
            m.vote_count 
          ));
        })
        .catch(error => {
          console.error('Error fetching movies:', error);
          throw error; 
        });
      }
      else{
        return fetch(`${this.baseUrl}/discover/movie?api_key=${this.apiKey}`)
        .then(res => res.json())
        .then(data => {
          return data.results.map((m: any) => new Movie(
            m.title,
            m.overview,
            m.vote_average,
            m.release_date,
            m.backdrop_path,
            m.poster_path,
            m.vote_count 
          ));
        })
        .catch(error => {
          console.error('Error fetching movies:', error);
          throw error; 
        });
      }
     
    }
  }