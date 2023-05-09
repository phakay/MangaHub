var ChapterService = function () {

    var removeChapterForManga = function (keycode, done, fail) {
        $.ajax({
            url: "/api/chapters?key=" + keycode,
            method: "DELETE"

        }).done(done).fail(fail);
    };

    return { removeChapterForManga: removeChapterForManga };

}();

