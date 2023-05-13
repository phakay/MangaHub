var ReadingsController = function (readingService) {
    
    var init = function (container) {
        $(container).on("click", ".js-toggle-reading", toggleReading);
    };

    var toggleReading = function (e) {
        var link = $(this);
        var id = link.attr("data-manga-id");
        var title = '';
        var done = function () {
            link.toggleClass('btn-info')
                .toggleClass('btn-default')
                .prop('title', title);

            link.children('i')
                .toggleClass('fa-book-reader')
                .toggleClass('fa-book');
        };

        if (link.hasClass('btn-default')) {
            readingService.add(id, done, fail);
            title = 'Reading';
        }
        else {
            readingService.remove(id, done, fail);
            title = 'Read';
        }
    };


    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };

}(ReadingService);
