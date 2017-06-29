var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Profile;
        (function (Profile) {
            var answers = (function () {
                function answers() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                }
                answers.prototype.execute = function () {
                    this.bindViewModel();
                };
                answers.prototype.bindViewModel = function () {
                };
                return answers;
            }());
            Profile.answers = answers;
            var ask = (function () {
                function ask() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                }
                ask.prototype.execute = function () {
                    this.renderUserTags();
                    this.initcontrollers();
                };
                ask.prototype.search = function (q) {
                };
                ask.prototype.renderUserTags = function () {
                };
                ask.prototype.initcontrollers = function () {
                };
                return ask;
            }());
            Profile.ask = ask;
            var criends = (function () {
                function criends() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                }
                criends.prototype.execute = function () {
                    this.bindViewModel();
                };
                criends.prototype.bindViewModel = function () {
                };
                return criends;
            }());
            Profile.criends = criends;
            var previewPage = (function () {
                function previewPage() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.cm = new Alpha.Utility.comman();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                    this.userid = '';
                    this.userid = this.cm.getQueryString('userid');
                }
                previewPage.prototype.execute = function () {
                    this.bindViewModel();
                };
                previewPage.prototype.renderUserTags = function (r) {
                    var _this = this;
                    $("#usertags_preview").html(Alpha.Utility.comman.loading);
                    var d = [];
                    d.push(r);
                    var templateContent = $("#userTags-template").html();
                    var template = kendo.template(templateContent);
                    var result = kendo.render(template, d);
                    $("#usertags_preview").html(result);
                    $('.tags').off('click').on('click', function (e) {
                        var $t = $(e.target);
                        _this.cm.addRemoveTags($t.data('tagid'), $t.html(), function () {
                        });
                    });
                };
                previewPage.prototype.renderUserContacts = function (r) {
                    $("#contactinfopreview").html(Alpha.Utility.comman.loading);
                    var d = [];
                    d.push(r);
                    var templateContent = $("#userContact-template").html();
                    var template = kendo.template(templateContent);
                    var result = kendo.render(template, d);
                    $("#contactinfopreview").html(result);
                };
                previewPage.prototype.bindViewModel = function () {
                    var _this = this;
                    this.ajax.get("/api/v1/userprofile/preview?guid=" + this.userid, null, null, '', function (re) {
                        var e = re.BasicInfo;
                        var viewModel = kendo.observable({
                            Followings: e.Followings,
                            Followers: e.Followings,
                            Asks: e.Asks,
                            Answers: e.Answers,
                            Country: e.Country,
                            MaritalStatus: e.MaritalStatus,
                            Tags: e.Tags,
                            Gender: e.Gender,
                            Name: e.Name,
                            Dob: kendo.toString(new Date(e.Dob), 'd'),
                            Bio: e.Bio,
                            ProfileImage: e.ProfileImage,
                            IsMine: !e.IsMine
                        });
                        kendo.bind($("#priviewpage"), viewModel);
                        $('#crienduserid').val(_this.userid);
                        $('#criendname').val(e.Name);
                        $('#criendprofileimage').val(e.ProfileImage);
                        _this.renderUserTags(re.UserTags);
                        _this.renderUserContacts(re.UserContacts);
                    });
                };
                return previewPage;
            }());
            Profile.previewPage = previewPage;
            var profileAskPost = (function () {
                function profileAskPost() {
                    this.askpost = new Alpha.Post.ask();
                }
                profileAskPost.prototype.execute = function () {
                    this.askpost.execute();
                };
                return profileAskPost;
            }());
            Profile.profileAskPost = profileAskPost;
            var messages = (function () {
                function messages() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.cm = new Alpha.Utility.comman();
                }
                messages.prototype.execute = function () {
                    this.userid = this.cm.getQueryString('userid');
                    this.bindViewModel();
                    this.loadChats();
                };
                messages.prototype.loadChats = function () {
                    var search = {
                        ToUser: this.userid,
                        Skip: 0,
                        Take: 20
                    };
                    this.ajax.post('/api/v1/user/message/read', search, null, 'chat loaded', function (e) {
                        console.log(e);
                        var template = kendo.template($("#chats-template").html());
                        var result = template(e);
                        $("#chatContent").html(result);
                    });
                };
                messages.prototype.bindViewModel = function () {
                    var _this = this;
                    var viewModel = kendo.observable({
                        ChatWith: 'Chamith',
                        Message: '',
                        ProfileImage: '',
                        ToUser: this.userid,
                        send: function (el) {
                            _this.ajax.post('/api/v1/user/message', viewModel, el, 'sent', function (e) {
                                var chat = {
                                    FromUser: $('#meuserid').val(),
                                    ToUser: _this.userid,
                                    Message: viewModel.get('Message'),
                                    SendDate: Date.now,
                                    ProfileImage: $('meprofileimage').val(),
                                    IsMyChat: true
                                };
                                var data = [];
                                data.push(chat);
                                var template = kendo.template($("#chats-template").html());
                                var result = template(data);
                                $("#chatContent").append(result);
                                viewModel.set('Message', '');
                            });
                        }
                    });
                    kendo.bind($("#messaging"), viewModel);
                };
                return messages;
            }());
            Profile.messages = messages;
        })(Profile = User.Profile || (User.Profile = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=profile.js.map