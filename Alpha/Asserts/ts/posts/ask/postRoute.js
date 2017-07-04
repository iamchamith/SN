var Alpha;
(function (Alpha) {
    var Post;
    (function (Post) {
        var templateLoader = (function ($, host) {
            $("#placeholder").html(Alpha.Utility.comman.loading);
            return {
                loadExtTemplate: function (path, callback) {
                    var tmplLoader = $.get(path)
                        .done(function (result) {
                        $("#placeholder").html(result);
                        callback();
                    })
                        .fail(function (result) {
                        alert("Error Loading Templates -- TODO: Better Error Handling");
                    });
                    tmplLoader.always(function () {
                        $(host).trigger("TEMPLATE_LOADED", [path]);
                    });
                }
            };
        })(jQuery, document);
        $(document).ready(function () {
            $('#placeholder').css('padding', '0');
            var a;
            var cm = new Alpha.Utility.comman();
            var type = cm.getQueryString('type');
            var templateUrl = '';
            if (type === "answers") {
                templateUrl = '/asserts/ts/posts/ask/templates/searchResult.template.html';
            }
            else {
                templateUrl = '/asserts/ts/posts/ask/templates/searchResult.template.html';
            }
            templateLoader.loadExtTemplate(templateUrl, function () {
                a = new Alpha.Post.ask();
                a.execute();
                $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                $('#seachpostresult').animateCss(Alpha.Utility.comman.animateType);
                return;
            });
        });
    })(Post = Alpha.Post || (Alpha.Post = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=postRoute.js.map