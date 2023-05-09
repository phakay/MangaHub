var MangaService = function () {

    var deleteManga = function (id, done, fail) {
        $.ajax({
            url: "/api/mangas/" + id,
            method: "DELETE"

        }).done(done).fail(fail);
    };

    return { deleteManga: deleteManga };

}();

