var get_comments = function (article) {
    return new Promise(resolve => {
        $.getJSON("/Articles/Comments/" + article, function (comments) {
            resolve(comments)
        })
    })
}

var delete_comment = function (id) {
    return new Promise(resolve => {
        $.ajax({
            url: "/api/Comments/" + id,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                resolve()
            }
        })
    })
}

var new_comment = function (author, content, article_id) {
    return new Promise(resolve => {
        var data = JSON.stringify({ "Author": author, "Content": content, "ArticleId": article_id })
        $.ajax({
            url: "/api/Comments",
            type: "POST",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                resolve()
            }
        })
    })
}