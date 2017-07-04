/// <reference path="authentication.ts" />
var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Authentication;
        (function (Authentication) {
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
                            // window.location.href = 'http://localhost:57449';
                        });
                        tmplLoader.always(function () {
                            $(host).trigger("TEMPLATE_LOADED", [path]);
                        });
                    }
                };
            })(jQuery, document);
            $(document).ready(function () {
                var a;
                var router = new kendo.Router({
                    init: function () {
                        templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.htm", function () {
                            $('#welcome').animateCss('pulse');
                        });
                    }
                });
                router.route("/", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.html", function () {
                        $('#welcome').animateCss('pulse');
                    });
                });
                router.route("/welcome", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.html", function () {
                        $('#welcome').animateCss('pulse');
                    });
                });
                router.route("/join", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/join.template.html", function () {
                        a = new Alpha.User.Authentication.login();
                        a.execute();
                        a = new Alpha.User.Authentication.register();
                        a.execute();
                        $('#join').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.start();
                router.navigate("/welcome");
            });
        })(Authentication = User.Authentication || (User.Authentication = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=authenticationRoute.js.map