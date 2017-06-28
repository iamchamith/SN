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
                var obj;
                var router = new kendo.Router({});
                router.route("/", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", function () {
                        $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                        $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                        obj = new Alpha.User.Settings.previewPage();
                        obj.execute();
                        $('#priviewpage').animateCss('pulse');
                    });
                });
                router.route("/preview", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/preview.template.html", function () {
                        obj = new Alpha.User.Settings.previewPage();
                        obj.execute();
                        $('#priviewpage').animateCss('pulse');
                    });
                });
                router.route("/basic", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/basic.template.html", function () {
                        obj = new Alpha.User.Settings.basic();
                        obj.execute();
                        $('#basic').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/contacts", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/userContacts.template.html", function () {
                        obj = new Alpha.User.Settings.userContact();
                        obj.execute();
                        $('#contacts').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/tags", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/tags.template.html", function () {
                        obj = new Alpha.User.Settings.tags();
                        obj.execute();
                        $('#usertags').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/changepassword", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/changepassword.template.html", function () {
                        obj = new Alpha.User.Settings.changePassword();
                        obj.execute();
                        $('#changepassword').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/image", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/changeprofileimage.template.html", function () {
                        obj = new Alpha.User.Settings.changeProfileImage();
                        obj.execute();
                        $('#profileImage').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/preferences", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/userpreferences.template.html", function () {
                        obj = new Alpha.User.Settings.preferences();
                        obj.execute();
                        $('#preferences').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.route("/sendmessage", function () {
                    templateLoader.loadExtTemplate("../asserts/ts/user/settings/templates/message.template.html", function () {
                        obj = new Alpha.User.Settings.messages();
                        obj.execute();
                        $('#messaging').animateCss(Alpha.Utility.comman.animateType);
                    });
                });
                router.start();
            });
        })(Settings = User.Settings || (User.Settings = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=settingsRoute.js.map