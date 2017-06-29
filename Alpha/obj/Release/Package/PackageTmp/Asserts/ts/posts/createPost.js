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
            }
            postCreations.prototype.execute = function () {
                this.initControllers();
            };
            postCreations.prototype.initControllers = function () {
                var _this = this;
                function getBase64(file, callback) {
                    var reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = function () {
                        callback(reader.result);
                    };
                    reader.onerror = function (error) {
                        console.log('Error: ', error);
                    };
                }
                $('.askQuestionMenu').off('click').on('click', function () {
                    _this.ajax.get('/api/v1/tag/read', null, null, "", function (e) {
                        $('#model-askQuestion').modal('show').appendTo('body');
                        _this.viewModel = kendo.observable({
                            Topic: '',
                            Description: '',
                            Tagss: e,
                            Tag: '',
                            IsAnonymas: false,
                            Vs1Data: '/asserts/images/dragdrop.png',
                            Vs2Data: '/asserts/images/dragdrop.png',
                            AskCommentImage: '/asserts/images/dragdrop.png',
                            askcomment: function (el) {
                                _this.ajax.post('/api/v1/post/comment', _this.viewModel, el, "success share", function () {
                                    $('#model-askQuestion').modal('hide');
                                });
                            },
                            askpoll: function (el) {
                                _this.ajax.post('/api/v1/post/poll', _this.viewModel, el, "success share", function () {
                                    $('#model-askQuestion').modal('hide');
                                });
                            },
                            askquestion: function (el) {
                                _this.ajax.post('/api/v1/post/question', _this.viewModel, el, "success share", function () {
                                    $('#model-askQuestion').modal('hide');
                                });
                            }
                        });
                        kendo.bind($("#model-askQuestion"), _this.viewModel);
                    });
                });
                $('#pollImgVs1').on('change', function (e) {
                    getBase64(e.target.files[0], function (r) {
                        _this.viewModel.set('Vs1Data', r);
                    });
                });
                $('#pollImgVs2').on('change', function (e) {
                    getBase64(e.target.files[0], function (r) {
                        _this.viewModel.set('Vs2Data', r);
                    });
                });
                $('#askcommentImg').on('change', function (e) {
                    getBase64(e.target.files[0], function (r) {
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