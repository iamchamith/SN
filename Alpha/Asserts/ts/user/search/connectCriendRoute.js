var Alpha;
(function (Alpha) {
    var Criends;
    (function (Criends) {
        var Search;
        (function (Search) {
            var templateLoader = (function ($, host) {
                $("#placeholder").html("<img src='/asserts/images/loader.gif'/>");
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
                var templateUrl = '/asserts/ts/user/search/templates/searchResult.template.html';
                templateLoader.loadExtTemplate(templateUrl, function () {
                    var searchCriends = new Alpha.Criends.Search.searchCriends();
                    searchCriends.execute();
                    $('#criendsearch').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                    $('#searchcriendplaceholder').animateCss(Alpha.Utility.comman.animateType);
                    return;
                });
            });
        })(Search = Criends.Search || (Criends.Search = {}));
    })(Criends = Alpha.Criends || (Alpha.Criends = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=connectCriendRoute.js.map