module Alpha.Post {
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
        $('#placeholder').css('padding', '0');
        let a: Alpha.Post.post;
        let cm = new Alpha.Utility.comman();
        let type = $.trim(cm.getQueryString('type').toLowerCase());
        $('.menuleftx').removeClass('active');
        let menuindex = 0;
        if (type === 'answers') {
            menuindex = 2;
        } else if (type == 'ask') {
            menuindex = 1;
        } else {
            menuindex = 0;
        }
        $($('.menuleftx').get(menuindex)).addClass('active');
        let templateUrl: string = '';
        templateUrl = '/asserts/ts/posts/templates/searchResult.template.html';
        templateLoader.loadExtTemplate(templateUrl, () => {
            a = new Alpha.Post.ask();
            a.execute();
            $('#leftmenu').animateCss(Alpha.Utility.comman.animateTypeLeftMenu);
            $('#seachpostresult').animateCss(Alpha.Utility.comman.animateType);
            return;
        });

    });
}