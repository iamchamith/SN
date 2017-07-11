/// <reference path="authentication.ts" />

module Alpha.User.Authentication {
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
                    })
                tmplLoader.always(function () {
                    $(host).trigger("TEMPLATE_LOADED", [path]);
                });
            }
        };
    })(jQuery, document);
    $(document).ready(() => {
        let a: Alpha.User.Authentication.authenticate;
        let template = '../asserts/ts/user/authentication/templates/join.template.html';
        var router = new kendo.Router({
            init: function () {
                templateLoader.loadExtTemplate(template, () => {
                    animi();
                });
            }
        });
        router.route("/", function () {
            templateLoader.loadExtTemplate(template, () => {
                animi();
            });
        });
        router.route("/join", function () {
            templateLoader.loadExtTemplate(template, () => {
                animi();
            });
        });
        function animi() {
            a = new Alpha.User.Authentication.login();
            a.execute();
            a = new Alpha.User.Authentication.register();
            a.execute();
            $('#join').animateCss(Alpha.Utility.comman.animateType);
        }
        router.start();
        router.navigate("/welcome");
    });
}