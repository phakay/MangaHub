var FollowingsController = function (followingService) {
    
    var init = function (container) {
        $(container).on("click", ".js-toggle-following", togglefollowing);
    };

    var togglefollowing = function (e) {
        var link = $(this);
        var id = link.attr("data-artist-id");
        var title = link.prop('title');
        var done = function () {
            link.toggleClass('btn-info')
                .toggleClass('btn-default')
                .prop('title', title);

            link.children('i')
                .toggleClass('fa-plus')
                .toggleClass('fa-minus');
        };

        if (link.hasClass('btn-default')) {
            title = 'Following';
            followingService.add(id, done, fail);
            
        }
        else {
            title = 'Follow';
            followingService.remove(id, done, fail);
        }
    };


    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };

}(FollowingService);
