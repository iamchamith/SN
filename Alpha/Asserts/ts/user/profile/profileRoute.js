var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Profile;
        (function (Profile) {
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
                var a;
                var templateElementId;
                var templateUrl = '../asserts/ts/user/settings/templates/preview.template.html';
                templateLoader.loadExtTemplate(templateUrl, function () {
                    a = new Alpha.User.Profile.previewPage();
                    a.execute();
                    $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                    return;
                });
                $('.menu').off('click').on('click', function (e) {
                    var $type = $(e.target).data('menu');
                    switch ($type) {
                        case 'preview':
                            templateUrl = '../asserts/ts/user/settings/templates/preview.template.html';
                            a = new Alpha.User.Profile.previewPage();
                            $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                            break;
                        case 'ask':
                            templateUrl = '../asserts/ts/posts/ask/templates/searchResult.template.html';
                            a = new Alpha.User.Profile.profileAskPost();
                            break;
                        case 'anwers':
                            templateUrl = '../asserts/ts/posts/ask/templates/searchResult.template.html';
                            a = new Alpha.User.Profile.profileAskPost();
                            ;
                            break;
                        case 'criends':
                            templateUrl = '../asserts/ts/user/settings/templates/tags.template.html';
                            a = new Alpha.User.Settings.tags();
                            break;
                        case 'chat':
                            templateUrl = '../asserts/ts/user/profile/templates/message.template.html';
                            a = new Alpha.User.Profile.messages();
                            $('#messaging').animateCss(Alpha.Utility.comman.animateType);
                            break;
                        default:
                            break;
                    }
                    templateLoader.loadExtTemplate(templateUrl, function () {
                        a.execute();
                    });
                });
            });
        })(Profile = User.Profile || (User.Profile = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=profileRoute.js.map