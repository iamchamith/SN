﻿/// <reference path="authentication.ts" />

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
                       // alert("Error Loading Templates -- TODO: Better Error Handling");
                    })
                tmplLoader.always(function () {
                    $(host).trigger("TEMPLATE_LOADED", [path]);
                });
            }
        };
    })(jQuery, document);
    $(document).ready(() => {
        let a: Alpha.User.Authentication.authenticate;
        var router = new kendo.Router({
            init: function () {
                templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.htm", () => {
                    $('#welcome').animateCss('pulse');
                });
            }
        });
        router.route("/", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.html", () => {
                $('#welcome').animateCss('pulse');
            });
        });
        router.route("/welcome", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/welcome.template.html", () => {
                $('#welcome').animateCss('pulse');
            });
        });
        router.route("/join", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/authentication/templates/join.template.html", () => {
                a = new Alpha.User.Authentication.login();
                a.execute();
                a = new Alpha.User.Authentication.register();
                a.execute();
                $('#join').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.start();
    });
}