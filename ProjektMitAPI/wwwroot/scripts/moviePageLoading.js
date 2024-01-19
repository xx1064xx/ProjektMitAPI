/*
TODO'S:
    - Disney + logo
    - Sticky scroll up button
    - API für Filmverwaltung
*/

async function getMovieData() {
    try {
        const response = await fetch("https://localhost:7072/api/movie/");

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

async function fill_template() {

    

    try {
        const data = await getMovieData();

        console.log('Received data:', data);

        if (!data || !Array.isArray(data)) {
            console.error('Data or movies array is undefined or not an array.');
            return;
        }

        if (data.length === 0) {
            console.warn('No movies found in the data.');
            return;
        }

        var movieIdentifier = url.searchParams.get("movie");
        console.log('URL Movie Identifier:', movieIdentifier);

        console.log('Movies array:', data);

        var singleMovie = data.find(function (movie) {
            return movie.identifier === movieIdentifier;
        });

        if (!singleMovie) {
            console.error('Movie not found for identifier:', movieIdentifier);
            return; // Exit the function if movie is not found
        }

        // Überprüfe, ob der identifier vorhanden ist, bevor du die Bilder lädst
        if (singleMovie.identifier) {
            var releaseDateArray = singleMovie.releaseDate.split(", ");
            var dateObject = new Date(releaseDateArray[0], releaseDateArray[1] - 1, releaseDateArray[2]).getFullYear();
            singleMovie.releaseDate = dateObject;

            console.log('Formatted movie:', singleMovie);
            
            var template = Handlebars.compile(document.querySelector('#moviePageTemplate').innerHTML);
            console.log("Single Movie: ", singleMovie.identifier);
            var filled = template(singleMovie);

            document.querySelector(".moviePageMain").innerHTML = filled;
        } else {
            console.error('Movie identifier is missing.');
        }
    } catch (error) {
        console.error('Error in fill_template:', error);
    }
}


var url = new URL(window.location.href);
fill_template();


