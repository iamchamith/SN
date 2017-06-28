var Alpha;
(function (Alpha) {
    var Utility;
    (function (Utility) {
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
                    });
                    tmplLoader.always(function () {
                        $(host).trigger("TEMPLATE_LOADED", [path]);
                    });
                }
            };
        })(jQuery, document);
        var comman = (function () {
            function comman() {
                this.profileRederect = '/useracccount/userprofile?userid=';
                this.pop = $("#notification").kendoNotification().data("kendoNotification");
            }
            comman.prototype.logout = function () {
                var ajax = new Alpha.Utility.Ajax();
                ajax.post('/api/v1/auth/logout', null, null, function () {
                    window.location.href = "/useracccount/authentication";
                });
            };
            comman.prototype.initNotifications = function () {
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
            };
            comman.prototype.addRemoveTags = function (tagId, tagName, callback) {
                var _this = this;
                var ajax = new Alpha.Utility.Ajax();
                ajax.get("/api/v1/tag/read/" + tagId, null, null, function (e) {
                    var viewModel = kendo.observable({
                        modelcaption: (e.IsTagThere) ? 'Remove Tag' : 'Add Tag',
                        caption: ((e.IsTagThere) ? 'remove ' : 'add ') + tagName,
                        OwnerId: e.OwnerId,
                        Description: (e.Description == '') ? 'not mention' : e.Description,
                        IsTagThere: e.IsTagThere,
                        Url: (e.IsTagThere) ? '/api/v1/tag/remove/' : '/api/v1/tag/add/',
                        ProfileRederect: _this.profileRederect + e.OwnerId,
                        ProfileImage: e.OwnerProfileImage,
                        OwnerName: e.OwnerName,
                        action: function () {
                            ajax.post(viewModel.get('Url') + tagId, null, null, function () {
                                _this.pop.show(viewModel.get('IsTagThere') ? 'Tag removed' : 'Tag added', 'success');
                                $('#addremovetags-model').modal('hide');
                                callback();
                            });
                        }
                    });
                    kendo.bind($("#addremovetags-model"), viewModel);
                    $('#addremovetags-model').modal('show');
                });
            };
            comman.prototype.initControllers = function () {
                var _this = this;
                $("#usertags-askquestionmodel").kendoAutoComplete({
                    filter: "startswith",
                    placeholder: "Select tags...",
                    separator: ", "
                });
                $('#logout').on('click', function () {
                    _this.logout();
                });
            };
            comman.prototype.getQueryString = function (name) {
                var url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"), results = regex.exec(url);
                if (!results)
                    return null;
                if (!results[2])
                    return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            };
            comman.prototype.bindFunctions = function () {
                var _this = this;
                $('.help').off('click').on('click', function (e) {
                    var $ele = $(e.target);
                    _this.showHelp($ele.data('url'), $ele.data('caption'));
                });
            };
            comman.prototype.showHelp = function (url, caption) {
                templateLoader.loadExtTemplate(url, function () {
                    $('#helpCaption').html(caption);
                    $('#helpModel').modal('show');
                });
            };
            comman.prototype.sendRelationshipRequest = function (searchRequest, el) {
                var _this = this;
                var ajax = new Alpha.Utility.Ajax();
                function changeText(el) {
                    var $ele = $(el.target);
                    var text = '';
                    var type = $ele.data('type');
                    var isrelation = $ele.data('isrelation');
                    var userId = $ele.data('userid');
                    if (type == '0') {
                        var $x = $("#" + userId + "_following");
                        var $y = $("#" + userId + "_block");
                        if (isrelation) {
                            // remove relation
                            if ($x.hasClass('btn-success')) {
                                $x.removeClass('btn-success');
                            }
                            if (!$x.hasClass('btn-default')) {
                                $x.addClass('btn-default');
                            }
                            $x.data('isrelation', false);
                        }
                        else {
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
                    }
                    else if (type == '1') {
                        var $x = $("#" + userId + "_follower");
                        var $y = $("#" + userId + "_block");
                        if (isrelation) {
                            // remove relation
                            if ($x.hasClass('btn-success')) {
                                $x.removeClass('btn-success');
                            }
                            if (!$x.hasClass('btn-default')) {
                                $x.addClass('btn-default');
                            }
                            $x.data('isrelation', false);
                        }
                        else {
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
                    }
                    else {
                        var $x = $("#" + userId + "_block");
                        var $y = $("#" + userId + "_following");
                        var $z = $("#" + userId + "_follower");
                        if (isrelation) {
                            // remove relation
                            if ($x.hasClass('btn-success')) {
                                $x.removeClass('btn-success');
                            }
                            if (!$x.hasClass('btn-default')) {
                                $x.addClass('btn-default');
                            }
                            $x.data('isrelation', false);
                        }
                        else {
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
                ajax.post('/api/v1/criends/relationship', searchRequest, null, function () {
                    _this.pop.show(' saved', 'success');
                    changeText(el);
                });
            };
            return comman;
        }());
        comman.loading = "<img src='/asserts/images/loader.gif'/>";
        comman.animateType = 'bounceInDown';
        comman.animateTypeAfterSearch = 'bounceIn';
        comman.animateTypeAfterSave = 'pulse';
        comman.animateTypeLeftMenu = 'fadeInDown';
        Utility.comman = comman;
        $(document).ready(function () {
            var cm = new comman();
            cm.initControllers();
        });
    })(Utility = Alpha.Utility || (Alpha.Utility = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=comman.js.map