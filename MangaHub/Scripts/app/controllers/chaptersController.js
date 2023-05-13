var ChaptersController = function (chapterService) {
    
    var init = function (container) {
        $(container).on("click", ".js-delete-chapter", addDeleteHandler);
    };

    var addDeleteHandler = function (e) {
        if (confirm("Are you sure you want to delete this chapter?")) {
            var link = $(this);
            var keycode = link.attr("data-chapter-keycode");

            var done = function () {
                link.closest("tr").fadeOut(function () {
                    $(this).remove();
                    applyPagination();
                });
            };

            chapterService.removeChapterForManga(keycode, done, fail);
        }
    };


    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };
}(ChapterService);
