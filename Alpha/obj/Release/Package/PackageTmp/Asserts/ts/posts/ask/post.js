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
            ask.prototype.renderDoComment = function () {
                var $comment;
                $('.panel-google-plus > .panel-footer > .input-placeholder, .panel-google-plus > .panel-google-plus-comment > .panel-google-plus-textarea > button[type="reset"]').on('click', function (event) {
                    var $panel = $(this).closest('.panel-google-plus');
                    $comment = $panel.find('.panel-google-plus-comment');
                    $comment.find('.btn:first-child').addClass('disabled');
                    $comment.find('textarea').val('');
                    $panel.toggleClass('panel-google-plus-show-comment');
                    if ($panel.hasClass('panel-google-plus-show-comment')) {
                        $comment.find('textarea').focus();
                    }
                });
                $('.panel-google-plus-comment > .panel-google-plus-textarea > textarea').on('keyup', function (event) {
                    var $comment = $(this).closest('.panel-google-plus-comment');
                    $comment.find('button[type="submit"]').addClass('disabled');
                    if ($(this).val().length >= 1) {
                        $comment.find('button[type="submit"]').removeClass('disabled');
                    }
                });
            };
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
                this.renderDoComment();
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
            ask.prototype.initController = function () {
                $('.like').off('click').on('click', function () { alert('liked'); });
                $('.dislike').off('click').on('click', function () { alert('liked'); });
                $('.viewpost').off('click').on('click', function () {
                    $('#showpostinfo-model').modal('show');
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