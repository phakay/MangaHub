var ChaptersController = function (chapterService) {
    
    var init = function (container) {
        $(container).on("click", ".js-delete-chapter", addDeleteButton);
    };

    var link;

    var addDeleteButton = function (e) {
        if (confirm("Are you sure you want to delete this chapter?")) {
            link = $(this);
            var keycode = link.attr("data-chapter-keycode");

            chapterService.removeChapterForManga(keycode, done, fail);
        }
    };

    var done = function () {
        link.closest("tr").fadeOut(function () {
            $(this).remove();
            applyPagination();
        });
    };

    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };
}(ChapterService);
