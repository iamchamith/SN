var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Settings;
        (function (Settings) {
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
                var templateUrl = '../asserts/ts/user/settings/templates/preview.template.html';
                templateLoader.loadExtTemplate(templateUrl, function () {
                    a = new Aplha.User.Settings.previewPage();
                    a.execute();
                    return;
                });
                $('.menu').off('click').on('click', function (e) {
                    var $type = $(e.target).data('menu');
                    switch ($type) {
                        case 'preview':
                            templateUrl = '../asserts/ts/user/settings/templates/preview.template.html';
                            $('.menu').bind('click');
                            a = new Aplha.User.Settings.previewPage();
                            break;
                        case 'basic':
                            templateUrl = '../asserts/ts/user/settings/templates/basic.template.html';
                            a = new Aplha.User.Settings.basic();
                            break;
                        case 'contacts':
                            templateUrl = '../asserts/ts/user/settings/templates/userContacts.template.html';
                            a = new Aplha.User.Settings.userContact();
                            break;
                        case 'tags':
                            templateUrl = '../asserts/ts/user/settings/templates/tags.template.html';
                            a = new Aplha.User.Settings.tags();
                            break;
                        case 'changepassword':
                            templateUrl = '../asserts/ts/user/settings/templates/changepassword.template.html';
                            a = new Aplha.User.Settings.changePassword();
                            break;
                        default:
                            break;
                    }
                    templateLoader.loadExtTemplate(templateUrl, function () {
                        a.execute();
                    });
                });
            });
        })(Settings = User.Settings || (User.Settings = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=settingsRoute.js.map