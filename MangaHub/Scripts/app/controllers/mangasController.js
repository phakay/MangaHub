var MangasController = function (mangaService) {
    
    var init = function (container) {
        $(container).on("click", ".js-delete-manga", addDeleteHandler);
    };


    var addDeleteHandler = function (e) {
        if (confirm("Are you sure you want to delete this chapter?")) {
            link = $(this);
            var id = link.attr("data-manga-id");

            var done = function () {
                link.closest(".manga-pane").fadeOut(function () {
                    $(this).remove();
                });
            };

            mangaService.deleteManga(id, done, fail);
        }
    };


    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };
}(MangaService);
