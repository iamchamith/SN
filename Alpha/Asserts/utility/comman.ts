module Alpha.Utility {
    var templateLoader = (function ($, host) {
        //$("#htmlContent").html(Alpha.Utility.comman.loading);
        return {
            loadExtTemplate: function (path, callback) {
                var tmplLoader = $.get(path)
                    .done(function (result) {
                        $("#htmlContent").html(result);
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
    export class comman {
        public static loading: string = "<img src='/asserts/images/loader.gif'/>";
        public static animateType: string = 'bounceInDown';
        public static animateTypeAfterSearch: string = 'bounceIn';
        public static animateTypeAfterSave: string = 'pulse';
        public static animateTypeLeftMenu: string = 'fadeInDown';
        public profileRederect = '/useracccount/userprofile?userid=';
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        constructor() { }
        public logout() {
            let ajax = new Alpha.Utility.Ajax();
            ajax.post('/api/v1/auth/logout', null, null, () => {
                window.location.href = "/useracccount/authentication";
            });
        }
        public initNotifications(): kendo.ui.NotificationOptions {
            return {
                position: {
                    pinned: true,
                    top: 30,
                    right: 30
                },
                autoHideAfter: 0,
                stacking: "down",
                templates: [{
                    type: "info",
                    template: $("#emailTemplate").html()
                }, {
                    type: "error",
                    template: $("#errorTemplate").html()
                }, {
                    type: "upload-success",
                    template: $("#successTemplate").html()
                }]

            };
        }
        public addRemoveTags(tagId: number, tagName: string, callback: any) {
            let ajax = new Alpha.Utility.Ajax();
            ajax.get(`/api/v1/tag/read/${tagId}`, null, null, (e) => {
                var viewModel = kendo.observable({
                    modelcaption: (e.IsTagThere) ? 'Remove Tag' : 'Add Tag',
                    caption: ((e.IsTagThere) ? 'remove ' : 'add ') + tagName,
                    OwnerId: e.OwnerId,
                    Description: (e.Description == '') ? 'not mention' : e.Description,
                    IsTagThere: e.IsTagThere,
                    Url: (e.IsTagThere) ? '/api/v1/tag/remove/' : '/api/v1/tag/add/',
                    ProfileRederect: this.profileRederect + e.OwnerId,
                    ProfileImage: e.OwnerProfileImage,
                    OwnerName: e.OwnerName,
                    action: () => {
                        ajax.post(viewModel.get('Url') + tagId, null, null, () => {
                            this.pop.show(viewModel.get('IsTagThere') ? 'Tag removed' : 'Tag added', 'success');
                            $('#addremovetags-model').modal('hide');
                            callback();
                        });
                    }
                });
                kendo.bind($("#addremovetags-model"), viewModel);
                $('#addremovetags-model').modal('show');
            });
        }
        public initControllers() {
            $("#usertags-askquestionmodel").kendoAutoComplete({
                filter: "startswith",
                placeholder: "Select tags...",
                separator: ", "
            });
            $('#logout').on('click', () => {
                this.logout();
            });
        }
        public getQueryString(name: string) {
            var url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        public bindFunctions() {
            $('.help').off('click').on('click', (e) => {
                var $ele = $(e.target);
                this.showHelp($ele.data('url'), $ele.data('caption'));
            });
        }
        private showHelp(url: string, caption: string) {
            templateLoader.loadExtTemplate(url, () => {
                $('#helpCaption').html(caption);
                $('#helpModel').modal('show');
            });
        }

        public sendRelationshipRequest(searchRequest, el: any) {
            let ajax = new Alpha.Utility.Ajax();
            function changeText(el: any) {
                var $ele = $(el.target);
                let text: string = '';
                let type = $ele.data('type');
                let isrelation = $ele.data('isrelation');
                let userId = $ele.data('userid');
                if (type == '0') {
                    let $x = $(`#${userId}_following`);
                    let $y = $(`#${userId}_block`);
                    if (isrelation) {
                        // remove relation
                        if ($x.hasClass('btn-success')) {
                            $x.removeClass('btn-success');
                        }
                        if (!$x.hasClass('btn-default')) {
                            $x.addClass('btn-default');
                        }
                        $x.data('isrelation', false);
                    } else {
                        //create relation
                        if ($x.hasClass('btn-default')) {
                            $x.removeClass('btn-default');
                        }
                        if (!$x.hasClass('btn-success')) {
                            $x.addClass('btn-success');
                        }
                        if ($y.hasClass('btn-success')) {
                            $y.removeClass('btn-success');
                        }
                        if (!$y.hasClass('btn-default')) {
                            $y.addClass('btn-default');
                        }
                        $x.data('isrelation', true);
                        $y.data('isrelation', false);
                    }
                } else if (type == '1') {
                    let $x = $(`#${userId}_follower`);
                    let $y = $(`#${userId}_block`);
                    if (isrelation) {
                        // remove relation
                        if ($x.hasClass('btn-success')) {
                            $x.removeClass('btn-success');
                        }
                        if (!$x.hasClass('btn-default')) {
                            $x.addClass('btn-default');
                        }
                        $x.data('isrelation', false);
                    } else {
                        //create relation
                        if ($x.hasClass('btn-default')) {
                            $x.removeClass('btn-default');
                        }
                        if (!$x.hasClass('btn-success')) {
                            $x.addClass('btn-success');
                        }
                        if ($y.hasClass('btn-success')) {
                            $y.removeClass('btn-success');
                        }
                        if (!$y.hasClass('btn-default')) {
                            $y.addClass('btn-default');
                        }
                        $x.data('isrelation', true);
                        $y.data('isrelation', false);
                    }
                } else {
                    let $x = $(`#${userId}_block`);
                    let $y = $(`#${userId}_following`);
                    let $z = $(`#${userId}_follower`);
                    if (isrelation) {
                        // remove relation
                        if ($x.hasClass('btn-success')) {
                            $x.removeClass('btn-success');
                        }
                        if (!$x.hasClass('btn-default')) {
                            $x.addClass('btn-default');
                        }
                        $x.data('isrelation', false);
                    } else {
                        //create relation
                        if ($x.hasClass('btn-default')) {
                            $x.removeClass('btn-default');
                        }
                        if (!$x.hasClass('btn-success')) {
                            $x.addClass('btn-success');
                        }
                        if ($y.hasClass('btn-success')) {
                            $y.removeClass('btn-success');
                        }
                        if (!$y.hasClass('btn-default')) {
                            $y.addClass('btn-default');
                        }
                        if ($z.hasClass('btn-success')) {
                            $z.removeClass('btn-success');
                        }
                        if (!$z.hasClass('btn-default')) {
                            $z.addClass('btn-default');
                        }
                        $x.data('isrelation', true);
                        $y.data('isrelation', false);
                        $z.data('isrelation', false);
                    }
                }

            }
            ajax.post('/api/v1/criends/relationship', searchRequest, null, () => {
                this.pop.show(' saved', 'success');
                changeText(el);
            });
        }
    }

    $(document).ready(() => {
        let cm = new comman();
        cm.initControllers();
    });
}