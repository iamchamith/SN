/// <reference path="postviewmodel.ts" />
var Alpha;
(function (Alpha) {
    var Post;
    (function (Post) {
        var ask = (function () {
            function ask() {
                this.ajax = new Alpha.Utility.Ajax();
                this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                this.cm = new Alpha.Utility.comman();
                this.userid = this.cm.getQueryString('userid');
            }
            ask.prototype.execute = function () {
                this.initController();
                this.bindSearchViewModel();
                var search = {
                    Topic: '',
                    IsDateDesc: true,
                    Skip: 0,
                    Take: 10,
                    UserId: this.userid,
                    IsMyAnswers: false,
                    IsMyAsks: false,
                    IsNeedComments: true,
                    IsPoll: true,
                    IsQuestions: true,
                    Tags: []
                };
                this.bindSearchData(search);
            };
            ask.prototype.bindSearchViewModel = function () {
                var _this = this;
                this.ajax.get('/api/v1/tag/read', null, null, "", function (e) {
                    var viewModel = kendo.observable({
                        Reason: true,
                        Titile: '',
                        IsMyAnswers: false,
                        IsMyAsks: false,
                        IsNeedComments: true,
                        IsPoll: true,
                        IsQuestions: true,
                        Tagss: e,
                        Tag: '',
                        search: function (el) {
                            var search = {
                                Topic: viewModel.get('Titile'),
                                IsDateDesc: viewModel.get('Reason'),
                                UserId: _this.userid,
                                Skip: 0,
                                Take: 10,
                                IsMyAnswers: viewModel.get('IsMyAnswers'),
                                IsMyAsks: viewModel.get('IsMyAsks'),
                                IsNeedComments: viewModel.get('IsNeedComments'),
                                IsPoll: viewModel.get('IsPoll'),
                                IsQuestions: viewModel.get('IsQuestions'),
                                Tags: []
                            };
                            _this.bindSearchData(search);
                        }, reset: function () {
                            var search = {
                                Topic: '',
                                IsDateDesc: true,
                                Skip: 0,
                                Take: 10,
                                UserId: _this.userid,
                                IsMyAnswers: false,
                                IsMyAsks: false,
                                IsNeedComments: true,
                                IsPoll: true,
                                IsQuestions: true,
                                Tags: []
                            };
                            _this.bindSearchData(search);
                            viewModel.set('Titile', '');
                            viewModel.set('Reason', true);
                        }
                    });
                    kendo.bind($("#searchpostcontrollers"), viewModel);
                });
            };
            ask.prototype.changeLikeButtonState = function (buttonType, state, postid, el) {
                if (!state) {
                    $(el).removeClass('btn-primary');
                    $(el).addClass('btn-default');
                    $(el).children('span').html((Number($(el).children('span').html()) - 1).toString());
                }
                else {
                    var $like = $("#l_" + postid);
                    var $dislike = $("#dl_" + postid);
                    if (buttonType == 0) {
                        $like.removeClass('btn-default').addClass('btn-primary');
                        $like.children('span').html((Number($like.children('span').html()) + 1).toString());
                        $dislike.children('span').html((Number($dislike.children('span').html()) - 1).toString());
                    }
                    else {
                        $dislike.removeClass('btn-default').addClass('btn-primary');
                        $dislike.children('span').html((Number($dislike.children('span').html()) + 1).toString());
                        $like.children('span').html((Number($like.children('span').html()) - 1).toString());
                    }
                }
            };
            ask.prototype.initController = function () {
                var _this = this;
                $('.like').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $state = !$(el.target).data('state');
                    var data = {
                        PostId: $postid,
                        Type: 0,
                        IsSelect: $state
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        _this.changeLikeButtonState(0, $state, $postid, el);
                    });
                });
                $('.dislike').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $state = !$(el.target).data('state');
                    var data = {
                        PostId: $postid,
                        Type: 1,
                        IsSelect: $state
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        _this.changeLikeButtonState(1, $state, $postid, el);
                    });
                });
                $('.viewpost').off('click').on('click', function () {
                    $('#showpostinfo-model').modal('show');
                });
                $('.removepost').off('click').on('click', function (el) {
                    if (confirm('do you want to remove this post')) {
                        var pid_1 = $(el.target).data('postid');
                        _this.ajax.get("/api/v1/post/delete?item=" + pid_1, null, el, 'Removed', function () {
                            $('#p_' + pid_1).remove();
                        });
                    }
                });
                $('.sendpost').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $txt = $("#txt_" + $postid).val();
                    _this.ajax.post('', null, el, 'sent', function () {
                        alert('sent');
                    });
                });
            };
            ask.prototype.bindSearchData = function (e) {
                var _this = this;
                this.ajax.post('/api/v1/post/search', e, null, "search is complete", function (r) {
                    var d = [];
                    d.push(r);
                    var templateContent = $("#posts-template").html();
                    var template = kendo.template(templateContent);
                    var result = kendo.render(template, d);
                    $("#searchposts").html(result);
                    $('#seachpostresult').animateCss(Alpha.Utility.comman.animateTypeAfterSearch);
                    _this.initController();
                });
            };
            return ask;
        }());
        Post.ask = ask;
        var answers = (function () {
            function answers() {
                this.ajax = new Alpha.Utility.Ajax();
                this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
            }
            answers.prototype.execute = function () {
                this.initControllers();
                this.bindSearchData();
            };
            answers.prototype.initControllers = function () {
                var _this = this;
                $('#resetSearch').off('click').on('click', function () {
                    _this.bindSearchData();
                });
            };
            answers.prototype.bindSearchData = function () {
                var _this = this;
                this.ajax.get('/api/v1/criends/search/looksup', null, null, "", function (e) {
                    _this.ajax.get('/api/v1/tag/read', null, null, "", function (e1) {
                        console.log(e1);
                        var viewModel = kendo.observable({
                            Countries: e.Countries,
                            Country: e.Country,
                            Genders: e.Genders,
                            Status: e.Status,
                            Gender: e.Gender,
                            MaritalStatus: e.MaritalStatus,
                            Name: '',
                            Tags: e1,
                            Tag: '',
                            Search: function (el) {
                                $("#searchcriends").html(Alpha.Utility.comman.loading);
                                var search = {
                                    Name: viewModel.get('Name'),
                                    Country: viewModel.get('Country'),
                                    Sex: viewModel.get('Gender'),
                                    MaritalStatus: viewModel.get('MaritalStatus'),
                                };
                                _this.ajax.post('/api/v1/criends/search', search, el, "", function (r) {
                                    console.log(r);
                                    var d = [];
                                    d.push(r);
                                    var templateContent = $("#searchCriends-template").html();
                                    var template = kendo.template(templateContent);
                                    var result = kendo.render(template, d);
                                    $("#searchcriends").html(result);
                                    $('#searchCount').html(r.length);
                                });
                            }
                        });
                        kendo.bind($("#criendsearch"), viewModel);
                    });
                });
            };
            return answers;
        }());
        Post.answers = answers;
    })(Post = Alpha.Post || (Alpha.Post = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=post.js.map