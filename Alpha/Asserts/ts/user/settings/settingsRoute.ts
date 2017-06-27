 module Alpha.User.Settings {
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
                    })
                tmplLoader.always(function () {
                    $(host).trigger("TEMPLATE_LOADED", [path]);
                });
            }
        };
    })(jQuery, document);
    $(document).ready(() => {
        let routeurl: Alpha.User.Settings.settings;
        var router = new kendo.Router({});
        router.route("/", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", () => {
                $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                routeurl = new Alpha.User.Settings.previewPage();
                routeurl.execute();
                $('#priviewpage').animateCss('pulse');
            });
        });
        router.route("/preview", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", () => {
                routeurl = new Alpha.User.Settings.previewPage();
                routeurl.execute();
                $('#priviewpage').animateCss('pulse');
            });
        });
        router.route("/basic", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/basic.template.html", () => {
                routeurl = new Alpha.User.Settings.basic();
                routeurl.execute();
                $('#basic').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/contacts", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/userContacts.template.html", () => {
                routeurl = new Alpha.User.Settings.userContact();
                routeurl.execute();
                $('#contacts').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/tags", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/tags.template.html", () => {
                routeurl = new Alpha.User.Settings.tags();
                routeurl.execute();
                $('#usertags').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/changepassword", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/changepassword.template.html", () => {
                routeurl = new Alpha.User.Settings.changePassword();
                routeurl.execute();
                $('#changepassword').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/image", function () {
            templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/changeprofileimage.template.html", () => {
                routeurl = new Alpha.User.Settings.changeProfileImage();
                routeurl.execute();
                $('#profileImage').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.start();
    });
}