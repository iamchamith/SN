var Alpha;
(function (Alpha) {
    var Util;
    (function (Util) {
        Util.templateLoader = (function ($, host) {
            return {
                loadExtTemplate: function (path, callback) {
                    var tmplLoader = $.get(path)
                        .done(function (result) {
                        $("#content").html(result);
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
    })(Util = Alpha.Util || (Alpha.Util = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=route.js.map