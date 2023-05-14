var FollowingService = function () {

    var remove = function (id, done, fail) {
        $.ajax({
            url: "/api/followings/" + id,
            method: "DELETE"

        }).done(done).fail(fail);
    };

    var add = function (id, done, fail) {
        $.ajax({
            url: "/api/followings",
            data: { FolloweeId: id },
            method: "POST"

        }).done(done).fail(fail);
    };

    return {
        remove: remove,
        add: add
    };

}();

