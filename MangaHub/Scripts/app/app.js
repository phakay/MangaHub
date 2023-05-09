function applyPagination() {
    var maxRows = 10;

    $('.manga-table').each(function () {
        var currentTable = this;
        var parent = $(this).parent();
        $(parent).find('.pagination').html('');
        var totalRows = $(this).children('tbody').children('tr').length;

        showNRowsOfTable(currentTable, maxRows, 1);
        showRowCount(parent, maxRows, 1, totalRows);

        if (totalRows > maxRows) {
            var pages = Math.ceil(totalRows / maxRows);
            for (i = 1; i <= pages; i++) {
                $(parent).find('.pagination')
                    .append(`<li data-page="${i}"><span>${i}<span class="sr-only">(current)</span>
                            </span></li>`)
                    .show();
            }

            $(parent).find('.pagination li:first-child').addClass('active');

            $(parent).find('.pagination li').on('click', function (e) {
                e.preventDefault();
                var pageNum = $(this).attr('data-page');
                $(this).siblings('.pagination li').removeClass('active');
                $(this).addClass('active');

                showNRowsOfTable(currentTable, maxRows, pageNum);
                showRowCount(parent, maxRows, pageNum, totalRows);

            });
        }
    });
}

function showNRowsOfTable(tableEl, maxRows, pageNum) {
    let trIndex = 1;
    $(tableEl).children('tbody').children('tr').each(function () {
        if (trIndex > maxRows * pageNum || trIndex <= maxRows * (pageNum - 1))
            $(this).hide();
        else
            $(this).show();
        trIndex++;
    });

}

function showRowCount(parentEl, maxRows, pageNum, totalRows) {
    var end_index = (maxRows * pageNum > totalRows)? totalRows : maxRows * pageNum;
    var start_index = maxRows * (pageNum - 1) + parseFloat(1);
    var text = `${start_index} to ${end_index} of ${totalRows} entries`;
    $(parentEl).children('.row_count').html(text);
}