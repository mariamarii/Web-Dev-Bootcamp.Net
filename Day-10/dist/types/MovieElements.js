export class MovieElements {
    updateFeatured(movie) {
        const rating = document.getElementById("rating");
        const year = document.getElementById("year");
        const voteCount = document.getElementById("voteCount");
        const description = document.getElementById("description");
        const containerSection = document.querySelector(".container");
        const title = document.querySelector(".section .title");
        if (rating)
            rating.textContent = movie.rating.toFixed(1);
        if (year)
            year.textContent = new Date(movie.releaseDate).getFullYear().toString();
        if (voteCount)
            voteCount.textContent = movie.voteCount.toString();
        if (description)
            this.setDescription(description, movie.overview);
        if (containerSection)
            containerSection.style.backgroundImage = `url(${movie.backdropUrl})`;
        if (title)
            title.textContent = movie.title;
    }
    setDescription(descriptionElement, text) {
        const maxLength = 150; // Max length to show initially
        if (text.length > maxLength) {
            descriptionElement.textContent = text.slice(0, maxLength) + "...";
            const seeMoreLink = document.createElement("a");
            seeMoreLink.textContent = "See More";
            seeMoreLink.href = "#";
            seeMoreLink.style.color = "yellow";
            descriptionElement.appendChild(seeMoreLink);
            seeMoreLink.addEventListener("click", (event) => {
                event.preventDefault();
                this.expandDescription(descriptionElement, text);
            });
        }
        else {
            descriptionElement.textContent = text;
        }
    }
    expandDescription(descriptionElement, fullText) {
        descriptionElement.textContent = fullText;
        const seeLessLink = document.createElement("a");
        seeLessLink.textContent = "See Less";
        seeLessLink.href = "#";
        seeLessLink.style.color = "yellow";
        descriptionElement.appendChild(seeLessLink);
        seeLessLink.addEventListener("click", (event) => {
            event.preventDefault();
            this.collapseDescription(descriptionElement, fullText);
        });
    }
    collapseDescription(descriptionElement, fullText) {
        const maxLength = 150;
        descriptionElement.textContent = fullText.slice(0, maxLength) + "...";
        const seeMoreLink = document.createElement("a");
        seeMoreLink.textContent = "See More";
        seeMoreLink.style.color = "yellow";
        seeMoreLink.href = "#";
        descriptionElement.appendChild(seeMoreLink);
        seeMoreLink.addEventListener("click", (event) => {
            event.preventDefault();
            this.expandDescription(descriptionElement, fullText);
        });
    }
    renderMovieCards(movies, onCardFocus) {
        const container = document.getElementById("movie-slider");
        if (!container)
            return; // Early exit if the container is not found
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
        if (!prevBtn || !nextBtn)
            return; // Exit early if buttons are not found
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
        const searchIcon = document.getElementById('search-icon');
        if (searchIcon) {
            searchIcon.addEventListener('click', () => {
                const nav = document.querySelector('nav');
                if (nav) {
                    nav.classList.toggle('search-active');
                }
            });
        }
        updateSlider();
    }
}
