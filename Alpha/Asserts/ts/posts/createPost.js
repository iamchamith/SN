var Alpha;
(function (Alpha) {
    var Post;
    (function (Post) {
        var imagetype;
        (function (imagetype) {
            imagetype[imagetype["poll1"] = 0] = "poll1";
            imagetype[imagetype["poll2"] = 1] = "poll2";
            imagetype[imagetype["comment"] = 2] = "comment";
        })(imagetype || (imagetype = {}));
        var postCreations = (function () {
            function postCreations() {
                this.pop = $("#notification").kendoNotification().data("kendoNotification");
                this.ajax = new Alpha.Utility.Ajax();
                this.viewModel = new kendo.data.ObservableObject();
                this.cm = new Alpha.Utility.comman();
            }
            postCreations.prototype.execute = function () {
                this.initControllers();
            };
            postCreations.prototype.genaralValidation = function () {
                if ($.trim(this.viewModel.get('Topic')) == '') {
                    this.pop.show('please add the topic', 'info');
                    $('.topic').animateCss(Alpha.Utility.comman.animateTypeAttention);
                    return false;
                }
                return true;
            };
            postCreations.prototype.initControllers = function () {
                var _this = this;
                $('.askQuestionMenu').off('click').on('click', function () {
                    _this.ajax.get('/api/v1/tag/read', null, null, "", function (e) {
                        $('#model-askQuestion').modal('show').appendTo('body');
                        _this.viewModel = kendo.observable({
                            Topic: '',
                            Description: '',
                            Tagss: e,
                            Tag: '',
                            IsAnonymas: false,
                            TopicLength: 150,
                            MoreLength: 300,
                            Days: 5,
                            AskTabType: 0,
                            Vs1Data: '/asserts/images/dragdrop.png',
                            Vs2Data: '/asserts/images/dragdrop.png',
                            AskCommentImage: '/asserts/images/dragdrop.png',
                            gonext: function () {
                                $('#askquestiontab .nav-tabs a[href="#menu1"]').tab('show');
                            },
                            ask: function (el) {
                                var asktype = _this.viewModel.get('AskTabType');
                                var posturl = '/api/v1/post/question';
                                if (asktype == 1) {
                                    posturl = '/api/v1/post/poll';
                                }
                                else if (asktype == 2) {
                                    posturl = '/api/v1/post/comment';
                                }
                                if (_this.genaralValidation()) {
                                    _this.ajax.post(posturl, _this.viewModel, el, "success share", function () {
                                        $('#model-askQuestion').modal('hide');
                                        window.location.href = '/posts/post/index?type=ask';
                                    });
                                }
                            },
                            changetopic: function (el) {
                                _this.viewModel.set('TopicLength', 150 - $(el.target).val().length);
                            },
                            changeMore: function (el) {
                                _this.viewModel.set('MoreLength', 300 - $(el.target).val().length);
                            },
                            onDaysChange: function (el) {
                                _this.viewModel.get("Days");
                            }
                        });
                        kendo.bind($("#model-askQuestion"), _this.viewModel);
                    });
                });
                $('#asktabtype a[data-toggle="tab"]').on('click', function (e) {
                    _this.viewModel.set('AskTabType', $(e.target).closest('li').index());
                });
                $('#pollImgVs1').on('change', function (e) {
                    _this.cm.fileTo64BaseString(e.target.files[0], function (r) {
                        _this.viewModel.set('Vs1Data', r);
                    });
                });
                $('#pollImgVs2').on('change', function (e) {
                    _this.cm.fileTo64BaseString(e.target.files[0], function (r) {
                        _this.viewModel.set('Vs2Data', r);
                    });
                });
                $('#askcommentImg').on('change', function (e) {
                    _this.cm.fileTo64BaseString(e.target.files[0], function (r) {
                        _this.viewModel.set('AskCommentImage', r);
                    });
                });
            };
            return postCreations;
        }());
        Post.postCreations = postCreations;
        $(document).ready(function () {
            var p = new Alpha.Post.postCreations();
            p.execute();
        });
    })(Post = Alpha.Post || (Alpha.Post = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=createPost.js.map