var get_searched_articles = function (category, title, summary, date) {
    return new Promise(resolve => {
        $.ajax({
            type: "GET",
            url: "/Articles/Search/",
            data: { category: category, header: title, summary: summary, date: date },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (articles) {
                resolve(articles)
            }
        });
    })
}

var get_searched_comments = function (author, content, date) {
    return new Promise(resolve => {
        $.ajax({
            type: "GET",
            url: "/api/Comments/Search/",
            data: { Author: author, Content: content, DatePosted: date },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (comments) {
                resolve(comments)
            }
        });
    })
}