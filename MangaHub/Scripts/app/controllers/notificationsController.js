var NotificationsController = function (notificationService) {

    var _container;
    var init = function (container) {
        _container = container;
        notificationService.getNotifications(done, fail);
    };

    var done = function (notifications) {
        if (notifications.length == 0)
            return;

        $(_container).find('.js-notifications-count')
            .text(notifications.length)
            .removeClass('hide')
            .addClass('animate__animated animate__bounceInDown');

        $(_container).find('.notifications').popover({
            html: true,
            title: "Notifications",
            content: function () {
                var compiled = _.template($('#notifications-template').html());
                return compiled({notifications: notifications});
            },
            placement: "bottom",
            template: '<div class="popover popover-notifications" role="tooltip"><div class="arrow">' +
                '</div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'

        }).on('shown.bs.popover', function () {
            var done_ = function () {
                $(_container).find('.js-notifications-count')
                    .text('')
                    .addClass("hide");
            };
            notificationService.markAsRead(done_, fail);
        });
    };

    var fail = function (res) {
        alert("Something failed!: " + res.responseText);
    };

    return {
        init: init
    };

}(NotificationService);
