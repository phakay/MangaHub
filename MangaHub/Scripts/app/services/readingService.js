var ReadingService = function () {

    var remove = function (id, done, fail) {
        $.ajax({
            url: "/api/readings/" + id,
            method: "DELETE"

        }).done(done).fail(fail);
    };

    var add = function (id, done, fail) {
        $.ajax({
            url: "/api/readings",
            data: { MangaId: id },
            method: "POST"

        }).done(done).fail(fail);
    };

    return {
        remove: remove,
        add: add
    };

}();

