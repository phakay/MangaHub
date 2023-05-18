var NotificationService = function () {

    var getNotifications = function (done, fail) {
        $.getJSON("/api/notifications")
            .done(done).fail(fail);
    };

    var markAsRead = function (done, fail) {
        $.post("/api/notifications/markAsRead")
            .done(done).fail(fail);
    };

    return {
        getNotifications: getNotifications,
        markAsRead: markAsRead
    };

}();

