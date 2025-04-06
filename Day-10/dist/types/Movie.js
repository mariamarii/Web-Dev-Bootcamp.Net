export class Movie {
    constructor(title, overview, rating, releaseDate, backdropPath, posterPath, voteCount) {
        this.title = title;
        this.overview = overview;
        this.rating = rating;
        this.releaseDate = releaseDate;
        this.backdropPath = backdropPath;
        this.posterPath = posterPath;
        this.voteCount = voteCount;
    }
    get posterUrl() {
        return this.posterPath
            ? `https://image.tmdb.org/t/p/w500${this.posterPath}`
            : 'https://image.tmdb.org/t/p/original/gh4cZbhZxyTbgxQPxD0dOudNPTn.jpg';
    }
    get backdropUrl() {
        return this.backdropPath
            ? `https://image.tmdb.org/t/p/original${this.backdropPath}`
            : 'https://image.tmdb.org/t/p/original/qJzloL8O9YHhiWBrhlPfKAtZu2I.jpg';
    }
}
