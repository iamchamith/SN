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
                var routeurl;
                var router = new kendo.Router({});
                router.route("/", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", function () {
                        $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                        $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                        routeurl = new Alpha.User.Settings.previewPage();
                        routeurl.execute();
                        $('#priviewpage').animateCss('pulse');
                    });
                });
                router.route("/preview", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", function () {
                        routeurl = new Alpha.User.Settings.previewPage();
                        routeurl.execute();
                        $('#priviewpage').animateCss('pulse');
                    });
                });
                router.route("/basic", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/basic.template.html", function () {
                        routeurl = new Alpha.User.Settings.basic();
                        routeurl.execute();
                        $('#basic').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/contacts", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/userContacts.template.html", function () {
                        routeurl = new Alpha.User.Settings.userContact();
                        routeurl.execute();
                        $('#contacts').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/tags", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/tags.template.html", function () {
                        routeurl = new Alpha.User.Settings.tags();
                        routeurl.execute();
                        $('#usertags').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/changepassword", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/changepassword.template.html", function () {
                        routeurl = new Alpha.User.Settings.changePassword();
                        routeurl.execute();
                        $('#changepassword').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.start();
            });
        })(Settings = User.Settings || (User.Settings = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=settingsRoute.js.map