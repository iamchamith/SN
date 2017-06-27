var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Settings;
        (function (Settings) {
            var basic = (function () {
                function basic() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.cm = new Alpha.Utility.comman();
                    this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                }
                basic.prototype.execute = function () {
                    this.initControllers();
                    this.bindViewModel();
                    this.cm.bindFunctions();
                };
                basic.prototype.initControllers = function () {
                    $(".nav-tabs a[data-toggle=tab]").on("click", function (e) {
                        var pop2 = $("#notification").kendoNotification().data("kendoNotification");
                        pop2.show(' please send validate request', 'info');
                        e.preventDefault();
                        return false;
                    });
                    $("#basic").kendoValidator({
                        rules: {
                            customRule1: function (input) {
                                if (input.is("[name=Email]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule2: function (input) {
                                if (input.is("[name=Name]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                        },
                        messages: {
                            customRule1: "Email requred",
                            customRule2: "Name requred"
                        }
                    });
                };
                basic.prototype.bindViewModel = function () {
                    var _this = this;
                    this.ajax.get('/api/v1/user/settings/looksup', null, null, function (e1) {
                        _this.ajax.get('/api/v1/user/settings/basic', null, null, function (e2) {
                            var viewModel = kendo.observable({
                                Email: e2.Email,
                                Bio: e2.Bio,
                                Country: e2.Country,
                                Dob: new Date(e2.Dob),
                                Gender: e2.Gender,
                                Language: e2.Language,
                                MaritalStatus: e2.MaritalStatus,
                                Name: e2.Name,
                                IsValiedEmail: e2.IsValiedEmail,
                                Countries: e1.Countries,
                                States: e1.Status,
                                Genders: e1.Genders,
                                EmailValidationToken: '',
                                IsInvaliedEmail: !e2.IsValiedEmail,
                                save: function (el) {
                                    if ($("#basic").data("kendoValidator").validate()) {
                                        _this.ajax.post('/api/v1/user/settings/basic', viewModel, el, function (e) {
                                            _this.pop.show('saved', 'success');
                                        });
                                    }
                                },
                                viewValidateEmailModel: function () {
                                    $('.nav-tabs li:eq(0) a').tab('show');
                                    $('#model-validateEmail').modal('show').appendTo('body');
                                },
                                sendEmailValidationToken: function (el) {
                                    _this.pop.show(' please wait for email', 'info');
                                    _this.ajax.post('/api/v1/user/settings/requestvalidateemailtoken', null, el, function () {
                                        _this.pop.show(' Token sent success.please insert the token', 'success');
                                        $('.nav-tabs li:eq(1) a').tab('show');
                                    });
                                },
                                validateEmailToken: function (el) {
                                    _this.ajax.get('/api/v1/user/settings/validateemailvalidationtoken?token=' + viewModel.get('EmailValidationToken'), null, el, function () {
                                        _this.pop.show('valied email', 'success');
                                        window.location.reload(true);
                                    });
                                }
                            });
                            kendo.bind($("#basic"), viewModel);
                        });
                    });
                };
                return basic;
            }());
            Settings.basic = basic;
            var tags = (function () {
                function tags() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                }
                tags.prototype.execute = function () {
                    this.renderUserTags();
                    this.initcontrollers();
                    this.cm.bindFunctions();
                };
                tags.prototype.search = function (q) {
                    var _this = this;
                    this.ajax.get("/api/v1/tag/search?q=" + q, null, null, function (e) {
                        var $addTag = $('#addTag');
                        var $createandaddTag = $('#crateAndAddTag');
                        var $tagDescription = $('#tagDescription');
                        if (!$addTag.hasClass('hidden')) {
                            $addTag.addClass('hidden');
                        }
                        if (!$createandaddTag.hasClass('hidden')) {
                            $createandaddTag.addClass('hidden');
                        }
                        if (!$tagDescription.hasClass('hidden')) {
                            $tagDescription.addClass('hidden');
                        }
                        if (e.length == 0) {
                            $createandaddTag.removeClass('hidden');
                            $tagDescription.removeClass('hidden');
                            _this.pop.show('this Tag not founed.Click add button for add tag', 'info');
                        }
                        else {
                            $addTag.removeClass('hidden');
                        }
                        var $ms = $("#tags").data("kendoComboBox");
                        $ms.setDataSource(new kendo.data.DataSource({ data: e }));
                    });
                };
                tags.prototype.reset = function () {
                    $('#tagDescription').val('');
                    if (!$('#tagDescription').hasClass('hidden')) {
                        $('#tagDescription').addClass('hidden');
                    }
                    if (!$('#crateAndAddTag').hasClass('hidden')) {
                        $('#crateAndAddTag').addClass('hidden');
                    }
                    if (!$('#addTag').hasClass('hidden')) {
                        $('#addTag').addClass('hidden');
                    }
                    $("#tags").data('kendoComboBox').value('');
                };
                tags.prototype.renderUserTags = function () {
                    var _this = this;
                    this.ajax.get('/api/v1/tag/read', null, null, function (r) {
                        var d = [];
                        d.push(r);
                        var templateContent = $("#userTags-template").html();
                        var template = kendo.template(templateContent);
                        var result = kendo.render(template, d);
                        $("#userTags").html(result);
                        $('.tags').off('click').on('click', function (e) {
                            var $t = $(e.target);
                            _this.cm.addRemoveTags($t.data('tagid'), $t.html(), function () {
                                _this.renderUserTags();
                            });
                        });
                    });
                };
                tags.prototype.initcontrollers = function () {
                    var _this = this;
                    $("#tags").kendoComboBox({
                        dataTextField: "Text",
                        dataValueField: "Value",
                    });
                    $('#searchTags').off('click').on('click', function () {
                        _this.search($.trim($("#tags").data('kendoComboBox').text()));
                    });
                    $('#addTag').off('click').on('click', function (el) {
                        var tagid = $("#tags").data('kendoComboBox').value();
                        _this.ajax.post('/api/v1/tag/add/' + tagid, null, el, function () {
                            _this.pop.show('saved', 'success');
                            _this.reset();
                            _this.renderUserTags();
                        });
                    });
                    $('#crateAndAddTag').off('click').on('click', function (el) {
                        var tagid = $("#tags").data('kendoComboBox').value();
                        _this.ajax.post('/api/v1/tag/create/', {
                            TagName: tagid,
                            Description: $('#tagDescription').val()
                        }, el, function () {
                            _this.pop.show('saved', 'success');
                            _this.reset();
                            _this.renderUserTags();
                        });
                    });
                };
                return tags;
            }());
            Settings.tags = tags;
            var changePassword = (function () {
                function changePassword() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                }
                changePassword.prototype.execute = function () {
                    this.initControllers();
                    this.bindViewModel();
                    this.cm.bindFunctions();
                };
                changePassword.prototype.initControllers = function () {
                    $("#changepassword").kendoValidator({
                        rules: {
                            customRule1: function (input) {
                                if (input.is("[id=currentPassword]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule2: function (input) {
                                if (input.is("[name=newPassword]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule3: function (input) {
                                if (input.is("[name=confimnewPassword]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                        },
                        messages: {
                            customRule1: "Current Password requred",
                            customRule2: "New password requred",
                            customRule3: "Confirm new password requred"
                        }
                    });
                };
                changePassword.prototype.bindViewModel = function () {
                    var _this = this;
                    var viewModel = kendo.observable({
                        CurrentPassword: '',
                        NewPassword: '',
                        ConfirmNewPassword: '',
                        saved: function (el) {
                            if ($("#changepassword").data("kendoValidator").validate()) {
                                _this.ajax.post('/api/v1/user/settings/ChangePassword', viewModel, el, function (e) {
                                    viewModel.set('CurrentPassword', '');
                                    viewModel.set('NewPassword', '');
                                    viewModel.set('ConfirmNewPassword', '');
                                    _this.pop.show('saved', 'success');
                                });
                            }
                        }
                    });
                    kendo.bind($("#changepassword"), viewModel);
                };
                return changePassword;
            }());
            Settings.changePassword = changePassword;
            var previewPage = (function () {
                function previewPage() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.cm = new Alpha.Utility.comman();
                    this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                }
                previewPage.prototype.execute = function () {
                    this.bindViewModel();
                };
                previewPage.prototype.renderUserTags = function () {
                    var _this = this;
                    $("#usertags_preview").html(Alpha.Utility.comman.loading);
                    this.ajax.get('/api/v1/tag/read', null, null, function (r) {
                        var d = [];
                        d.push(r);
                        var templateContent = $("#userTags-template").html();
                        var template = kendo.template(templateContent);
                        var result = kendo.render(template, d);
                        $("#usertags_preview").html(result);
                        $('.tags').off('click').on('click', function (e) {
                            var $t = $(e.target);
                            _this.cm.addRemoveTags($t.data('tagid'), $t.html(), function () {
                                _this.renderUserTags();
                            });
                        });
                    });
                };
                previewPage.prototype.renderUserContacts = function () {
                    $("#contactinfopreview").html(Alpha.Utility.comman.loading);
                    this.ajax.get('/api/v1/user/settings/contactdetailssummery', null, null, function (r) {
                        var d = [];
                        d.push(r);
                        var templateContent = $("#userContact-template").html();
                        var template = kendo.template(templateContent);
                        var result = kendo.render(template, d);
                        $("#contactinfopreview").html(result);
                    });
                };
                previewPage.prototype.bindViewModel = function () {
                    this.ajax.get('/api/v1/user/settings/priviewpage', null, null, function (e) {
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
                        });
                        kendo.bind($("#priviewpage"), viewModel);
                    });
                    this.renderUserTags();
                    this.renderUserContacts();
                };
                return previewPage;
            }());
            Settings.previewPage = previewPage;
            var userContact = (function () {
                function userContact() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                }
                userContact.prototype.execute = function () {
                    this.renderContactTemplate();
                    this.bindViewModel();
                    this.cm.bindFunctions();
                };
                userContact.prototype.renderContactTemplate = function () {
                    $("#contactinfo").html(Alpha.Utility.comman.loading);
                    this.ajax.get('/api/v1/user/settings/contactdetailssummery', null, null, function (r) {
                        var d = [];
                        d.push(r);
                        var templateContent = $("#userContact-template").html();
                        var template = kendo.template(templateContent);
                        var result = kendo.render(template, d);
                        $("#contactinfo").html(result);
                    });
                };
                userContact.prototype.bindViewModel = function () {
                    var _this = this;
                    this.ajax.get('/api/v1/user/settings/usercontactslooksup', null, null, function (e) {
                        var viewModel = kendo.observable({
                            Contacts: e,
                            SocialNetwork: -1,
                            Key: '',
                            Url: '',
                            IsSaveVisible: false,
                            IsRemoveVisible: false,
                            onChange: function (arg) {
                                if (viewModel.get('SocialNetwork') == -1) {
                                    viewModel.set('IsSaveVisible', false);
                                    viewModel.set('IsRemoveVisible', false);
                                    viewModel.set('Key', '');
                                    viewModel.set('Url', '');
                                    return;
                                }
                                viewModel.set('IsSaveVisible', true);
                                _this.ajax.get('/api/v1/user/settings/contactdetails/' + viewModel.get('SocialNetwork'), null, null, function (e2) {
                                    if (e2.Key == null) {
                                        viewModel.set("Key", arg.sender._prev);
                                        // there is no recode
                                        viewModel.set('IsRemoveVisible', false);
                                    }
                                    else {
                                        viewModel.set('Key', e2.Key);
                                        viewModel.set('IsRemoveVisible', true);
                                    }
                                    viewModel.set('Url', e2.Url);
                                });
                            },
                            add: function (e) {
                                _this.ajax.post('/api/v1/user/settings/contactdetails', viewModel, e, function (e2) {
                                    _this.pop.show('saved', 'success');
                                    _this.renderContactTemplate();
                                    $('#contacts').animateCss(Alpha.Utility.comman.animateTypeAfterSave);
                                });
                            },
                            remove: function (e) {
                                _this.ajax.post('/api/v1/user/settings/contactdetails/' + viewModel.get('SocialNetwork'), null, e, function (e2) {
                                    _this.pop.show('removed', 'success');
                                    _this.renderContactTemplate();
                                    $('#contacts').animateCss(Alpha.Utility.comman.animateTypeAfterSave);
                                });
                            }
                        });
                        kendo.bind($("#contacts"), viewModel);
                    });
                };
                return userContact;
            }());
            Settings.userContact = userContact;
            var changeProfileImage = (function () {
                function changeProfileImage() {
                }
                changeProfileImage.prototype.execute = function () {
                    this.initControllers();
                };
                changeProfileImage.prototype.initControllers = function () {
                    $('#saveImage').off('click').on('click', function () {
                        var data = new FormData();
                        var d = $("#fileProfileImage").get(0);
                        var files = d.files;
                        // Add the uploaded image content to the form data collection
                        if (files.length > 0) {
                            data.append("UploadedImage", files[0]);
                        }
                        // Make Ajax request with the contentType = false, and procesDate = false
                        var ajaxRequest = $.ajax({
                            type: "POST",
                            url: "/api/v1/base/image",
                            contentType: false,
                            processData: false,
                            data: data
                        });
                        ajaxRequest.done(function (xhr, textStatus) {
                            // Do other operation
                        });
                    });
                };
                return changeProfileImage;
            }());
            Settings.changeProfileImage = changeProfileImage;
        })(Settings = User.Settings || (User.Settings = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=settings.js.map