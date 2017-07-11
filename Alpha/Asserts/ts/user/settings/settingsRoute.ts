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
     var templateLoaderHelp = (function ($, host) {
         $("#placeholder").html(Alpha.Utility.comman.loading);
         return {
             loadExtTemplate: function (path, callback) {
                 var tmplLoader = $.get(path)
                     .done(function (result) {
                         $("#infoz").html(result);
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
        let obj: Alpha.User.Settings.settings;
        var router = new kendo.Router({});
        $('.menuleftx').off('click').on('click', (e) => {
          //  window.location.href = $(e.target).children('a').attr('href');
        });
        router.route("/", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/preview.template.html", () => {
                $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
                $('#priviewpage').animateCss(Alpha.Utility.comman.animateType);
                obj = new Alpha.User.Settings.previewPage();
                obj.execute();
                $('#priviewpage').animateCss('pulse');
            });
        });
        router.route("/preview", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/preview.template.html", () => {
                obj = new Alpha.User.Settings.previewPage();
                obj.execute();
                $('#priviewpage').animateCss('pulse');
            });
        });
        router.route("/basic", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/basic.template.html", () => {
                obj = new Alpha.User.Settings.basic();
                obj.execute();
                $('#basic').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/contacts", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/userContacts.template.html", () => {
                obj = new Alpha.User.Settings.userContact();
                obj.execute();
                $('#contacts').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/tags", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/tags.template.html", () => {
                obj = new Alpha.User.Settings.tags();
                obj.execute();
                $('#usertags').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/changepassword", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/changepassword.template.html", () => {
                obj = new Alpha.User.Settings.changePassword();
                obj.execute();
                $('#changepassword').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/image", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/changeprofileimage.template.html", () => {
                obj = new Alpha.User.Settings.changeProfileImage();
                obj.execute();
                $('#profileImage').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/preferences", function () {
            templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/userpreferences.template.html", () => {
                obj = new Alpha.User.Settings.preferences();
                obj.execute();
                $('#preferences').animateCss(Alpha.Utility.comman.animateType);
            });
        });
        router.route("/info", function (params) {
            if ($.type(params.type) == 'undefined') {
                templateLoader.loadExtTemplate("/asserts/ts/user/settings/templates/help.template.html", () => {
                    $('#info').animateCss(Alpha.Utility.comman.animateType);
                    templateLoaderHelp.loadExtTemplate(`/asserts/ts/help/genaral/faq.template.html`, () => {
                        $('#infoz').animateCss(Alpha.Utility.comman.animateTypeAfterSearch);
                    });
                });
            } else {
                templateLoaderHelp.loadExtTemplate(`/asserts/ts/help/genaral/${params.type}.template.html`, () => {
                    $('#infoz').animateCss(Alpha.Utility.comman.animateTypeAfterSearch);
                });
            }
        });
        router.start();
        router.navigate("/preview");
        $('.leftmenu').off('click').on('click', (e) => {
            $('.list-group-item').removeClass('active');
            $(e.target).parent().addClass('active');
        });
    });
}