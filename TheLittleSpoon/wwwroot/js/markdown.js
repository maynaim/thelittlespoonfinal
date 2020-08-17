var showdown_converters = function (converter) {
    return [
        {
            type: 'html',
            regex: '<img src=(.*)>',
            replace: '<img class="img-fluid img-thumbnail" src=$1>'
        },
        {
            type: 'lang',
            regex: /\!\[VIDEO\]\(.*\)/,
            replace: function (match, converter, options) {
                var url = match.slice(9, -1)

                return '<div class="embed-responsive embed-responsive-16by9"><video controls><source src="' + url + '"></video></div>'

            }
        },
        {
            type: 'lang',
            regex: /\!\[TWITTER\]\(.*\)/,
            replace: function (match, converter, options) {
                var url = match.slice(11, -1)

                return "<div class='tweeter_tweet col-12' tweeter-url='" + url + "'><script async src='https://platform.twitter.com/widgets.js' charset='utf-8'></script></div>"
            }
        }
    ]
}

var make_markdown_editor = function (e, initial_content) {
    var simplemde = new SimpleMDE({
        element: e,
        insertTexts: {
            video: ["![VIDEO](", ")"],
            twitter: ["![TWITTER](", ")"]
        }
    });
    var str = decodeHtml(initial_content)
    simplemde.value(str);
}

var markdown_to_html = function (markdown_string) {
    var converter = new showdown.Converter({ extensions: [showdown_converters] }),
        text = decodeHtml(markdown_string),
        html = converter.makeHtml(text);

    return html;
}

var load_markdown_tweeter = function () {
    $(".tweeter_tweet").each(function (index) {
        console.log(index + ":" + $(this).attr("tweeter-url"));
        load_tweet($(this))
    });
}

function decodeHtml(html) {
    var txt = document.createElement("textarea");
    txt.innerHTML = html;
    return txt.value;
}

var load_tweet = function (obj) {
    console.log(obj)
    console.log(obj.attr("tweeter-url"))
    $.ajax({
        type: "GET",
        url: "https://publish.twitter.com/oembed?url=" + obj.attr("tweeter-url"),
        data: obj.attr("tweeter-url"),
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            obj.append(data['html']);
        }
    })
};