async function getMovieData() {
    const request = await fetch(`https://localhost:7072/api/movie/`, {
        method: 'GET'
    });
    const data = await request.json();
    return data;
}

function fill_template(fillId, moviesToFill) {

    const data = moviesToFill;



    var template = Handlebars.compile(document.querySelector('#movieTemplate').innerHTML);
    var filled = template(data);
    document.querySelector(fillId).innerHTML = filled;

    console.log(data);
    
}

function get_comingSoon(data) {

   
    var vorEinemMonat = new Date();
    vorEinemMonat.setMonth(vorEinemMonat.getMonth() - 1);

    // Filtere Filme mit releaseDate später als heute vor einem Monat
    var aktuelleFilme = data.filter(function (film) {
        // Wandle den String in ein Array von Zahlen um
        var datumTeile = film.releaseDate.split(',').map(function (item) {
            return parseInt(item, 10);
        });

        // Erstelle ein Date-Objekt aus dem Array
        var filmDatum = new Date(datumTeile[0], datumTeile[1] - 1, datumTeile[2]);

        return filmDatum > vorEinemMonat;
    });

    // Gib die gefundenen Filme auf der Konsole aus
    return aktuelleFilme;

}


async function loadIndexPageMovies() {
    const allMoviesdata = await getMovieData();
    fill_template("#suggestions", allMoviesdata);
    const aktuelleFilme = get_comingSoon(allMoviesdata);
    fill_template("#comingSoon", aktuelleFilme);
}


loadIndexPageMovies();