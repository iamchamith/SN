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
                var c = this.cm.getQueryString('type');
                if (c == 'ask') {
                    this.posttype = Post.Posttype.Ask;
                }
                else if (c == 'answer') {
                    this.posttype = Post.Posttype.Question;
                }
                else {
                    this.posttype = Post.Posttype.Feed;
                }
            }
            ask.prototype.execute = function () {
                this.initController();
                this.initControllerLikeDislike();
                this.bindSearchViewModel();
                var search = {
                    Topic: '',
                    IsDateDesc: true,
                    Skip: 0,
                    Take: 10,
                    UserId: this.userid,
                    IsMyAnswers: this.posttype == Post.Posttype.Question,
                    IsMyAsks: this.posttype == Post.Posttype.Ask,
                    IsNeedComments: true,
                    IsPoll: true,
                    IsQuestions: true,
                    Tags: [],
                    PostSearchType: this.posttype
                };
                this.bindSearchData(search);
            };
            ask.prototype.bindSearchViewModel = function () {
                var _this = this;
                this.ajax.get('/api/v1/tag/read', null, null, "", function (e) {
                    var viewModel = kendo.observable({
                        Reason: true,
                        Titile: '',
                        IsMyAnswers: _this.posttype == Post.Posttype.Question,
                        IsMyAsks: _this.posttype == Post.Posttype.Ask,
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
                                Tags: [],
                                PostSearchType: _this.posttype
                            };
                            _this.bindSearchData(search);
                        }, reset: function () {
                            var search = {
                                Topic: '',
                                IsDateDesc: true,
                                Skip: 0,
                                Take: 10,
                                UserId: _this.userid,
                                IsMyAnswers: _this.posttype == Post.Posttype.Question,
                                IsMyAsks: _this.posttype == Post.Posttype.Ask,
                                IsNeedComments: true,
                                IsPoll: true,
                                IsQuestions: true,
                                Tags: [],
                                PostSearchType: _this.posttype
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
            ask.prototype.initControllerLikeDislike = function () {
                var _this = this;
                $('.like').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $state = $(el.target).data('liketype');
                    var data = {
                        PostId: $postid,
                        Type: 1,
                        IsSelect: ($state == 'removedislike') ? false : true,
                        PostLikeModeType: 0 // is post or comment
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        //this.changeLikeButtonState(0, $state, $postid, el);
                    });
                });
                $('.dislike').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $state = !$(el.target).data('liketype');
                    var data = {
                        PostId: $postid,
                        Type: 1,
                        IsSelect: $state,
                        PostLikeModeType: 0
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        _this.changeLikeButtonState(1, $state, $postid, el);
                    });
                });
                $('.whodopostlike').off('click').on('çlick', function (el) {
                    _this.ajax.get('/api/v1topost/whodolikedislike', {
                        postid: $(el).data('postid'),
                        islike: $(el).data('type') === 'like'
                    }, el, '', function (r) {
                        console.log(r);
                    });
                });
            };
            ask.prototype.initController = function () {
                var _this = this;
                $('.viewpost').off('click').on('click', function () {
                    $('#showpostinfo-model').modal('show');
                });
                $('.removepost').off('click').on('click', function (el) {
                    var $self = _this;
                    swal({
                        title: "Are you sure?",
                        text: "Do you want to remove this post?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes",
                        closeOnConfirm: true
                    }, function () {
                        var pid = $(el.target).data('postid');
                        $self.ajax.get("/api/v1/post/delete?item=" + pid, null, el, 'Removed', function () {
                            $('#p_' + pid).remove();
                        });
                    });
                });
                $('.sendcomment').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $txt = $("#txt_" + $postid).val();
                    var $isanonymas = $("#chk_" + $postid).is(':checked');
                    var $commentContent = $("#c_" + $postid);
                    if ($.trim($txt) == '') {
                        _this.pop.show('please write a comment', 'info');
                        $("#txt_" + $postid).animateCss(Alpha.Utility.comman.animateTypeAttention);
                        return;
                    }
                    var data = {
                        Comment: $txt,
                        PostIdStr: $postid,
                        IsAnonymas: $isanonymas
                    };
                    _this.ajax.post('/api/v1/topost/comment', data, el, 'sent', function (r) {
                        var d = [];
                        d.push(r);
                        var template = kendo.template($("#postsComment-template").html());
                        var result = template(d);
                        $commentContent.append(result);
                        $("#txt_" + $postid).val('');
                        $("#chk_" + $postid).prop('checked', false);
                        _this.initController();
                    });
                });
                $('.showcomment').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('postid');
                    var $commentContent = $("#c_" + $postid);
                    $commentContent.html('loading...');
                    _this.ajax.get("/api/v1/topost/comment?postid=" + $postid, null, el, '', function (r) {
                        var template = kendo.template($("#postsComment-template").html());
                        var result = template(r);
                        $commentContent.html(result);
                        _this.initController();
                    });
                });
                $('.commentlike').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('commentid');
                    var $state = !$(el.target).data('state');
                    var data = {
                        PostId: $postid,
                        Type: 1,
                        IsSelect: $state,
                        PostLikeModeType: 1
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        //this.changeLikeButtonState(1, $state, $postid, el);
                    });
                });
                $('.commentdislike').off('click').on('click', function (el) {
                    var $postid = $(el.target).data('commentid');
                    var $state = !$(el.target).data('state');
                    var data = {
                        PostId: $postid,
                        Type: 1,
                        IsSelect: $state,
                        PostLikeModeType: 1
                    };
                    _this.ajax.post('/api/v1/topost/likedislike', data, el, 'saved', function (e) {
                        //this.changeLikeButtonState(1, $state, $postid, el);
                    });
                });
                $('.commentremove').off('click').on('click', function (el) {
                    if (confirm('do you want to remove this comment ?')) {
                        var pid_1 = $(el.target).data('commentid');
                        _this.ajax.post("/api/v1/topost/comment/remove", { CommentId: pid_1 }, el, 'Removed', function () {
                            $('#c_' + pid_1).remove();
                        });
                    }
                });
                $('.votepoll').off('click').on('click', function (el) {
                    var $t = $(el.target);
                    var $state = $t.data('state');
                    var $what = $t.data('what');
                    var $postid = $t.data('postid');
                });
                $('.commentanonymas').off('click').on('click', function (el) {
                    if ($(el.target).is(':checked')) {
                        _this.pop.show('comment as anonymas', 'info');
                    }
                    else {
                        _this.pop.show('comment as yourself', 'info');
                    }
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
                    $('#searchposts').animateCss(Alpha.Utility.comman.animateTypeAfterSearch);
                    _this.initController();
                    _this.initControllerLikeDislike();
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