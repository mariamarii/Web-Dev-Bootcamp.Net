import {IMovie } from "./IMovie";

export class Movie implements IMovie {
  constructor(
    public title: string,
    public overview: string,
    public rating: number,
    public releaseDate: string,
    public backdropPath: string,
    public posterPath: string,
    public voteCount : string
  ) {}

 

  
    get posterUrl(): string {
      return this.posterPath 
        ? `https://image.tmdb.org/t/p/w500${this.posterPath}` 
        : 'https://image.tmdb.org/t/p/original/gh4cZbhZxyTbgxQPxD0dOudNPTn.jpg';
    }
  
    get backdropUrl(): string {
      return this.backdropPath 
        ? `https://image.tmdb.org/t/p/original${this.backdropPath}` 
        : 'https://image.tmdb.org/t/p/original/qJzloL8O9YHhiWBrhlPfKAtZu2I.jpg';
    }
  }
  