module Alpha.User.Settings {

    export interface settings {
        execute();
    }

    export class basic implements settings {
        private ajax = new Alpha.Utility.Ajax();
        private cm = new Alpha.Utility.comman();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        public execute() {
            this.initControllers();
            this.bindViewModel();
            this.cm.bindFunctions();
        }
        private initControllers() {
            $(".nav-tabs a[data-toggle=tab]").on("click", function (e) {
                let pop2 = $("#notification").kendoNotification().data("kendoNotification");
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
        }
        private bindViewModel() {
            this.ajax.get('/api/v1/user/settings/looksup', null, null, "", (e1) => {
                this.ajax.get('/api/v1/user/settings/basic', null, null, "", (e2: user) => {
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
                        Employeement: e2.Employeement,
                        save: (el) => {
                            if ($("#basic").data("kendoValidator").validate()) {
                                this.ajax.post('/api/v1/user/settings/basic', viewModel, el, "Saved", (e) => {
                                });
                            }
                        },
                        viewValidateEmailModel: () => {
                            $('#validateemail .nav-tabs li:eq(0) a').tab('show');
                            $('#model-validateEmail').modal('show').appendTo('body');
                        },
                        sendEmailValidationToken: (el) => {
                            this.pop.show(' please wait for email', 'info');
                            this.ajax.post('/api/v1/user/settings/requestvalidateemailtoken', null, el,
                                'Token sent success.please insert the token', () => {
                                    $('#validateemail .nav-tabs li:eq(1) a').tab('show');
                                });
                        },
                        validateEmailToken: (el) => {
                            this.ajax.get('/api/v1/user/settings/validateemailvalidationtoken?token=' + viewModel.get('EmailValidationToken'),
                                null, el, 'validate email', () => {
                                    window.location.reload(true);
                                });
                        }
                    });
                    kendo.bind($("#basic"), viewModel);
                });
            });
        }

        constructor() { }
    }

    export class tags implements settings {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.renderUserTags();
            this.initcontrollers();
            this.cm.bindFunctions();
        }
        private search(q: string) {
            this.ajax.get(`/api/v1/tag/search?q=${q}`, null, null, '', (e) => {
                let $addTag = $('#addTag');
                let $createandaddTag = $('#crateAndAddTag');
                let $tagDescription = $('#tagDescription');
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
                    this.pop.show('this Tag not founed.Click add button for add tag', 'info');
                } else {
                    $addTag.removeClass('hidden');
                }
                let $ms = $("#tags").data("kendoComboBox");
                $ms.setDataSource(new kendo.data.DataSource({ data: e }));
            });
        }
        private reset() {
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
        }
        private renderUserTags() {
            this.ajax.get('/api/v1/tag/read', null, null, '', (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#userTags-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                $("#userTags").html(result);
                $('.tags').off('click').on('click', (e) => {
                    var $t = $(e.target);
                    this.cm.addRemoveTags($t.data('tagid'), $t.html(), () => {
                        this.renderUserTags();
                    });
                });
            });
        }
        private initcontrollers() {
            $("#tags").kendoComboBox({
                dataTextField: "Text",
                dataValueField: "Value",
            });
            $('#searchTags').off('click').on('click', () => {
                this.search($.trim($("#tags").data('kendoComboBox').text()));
            });
            $('#addTag').off('click').on('click', (el) => {
                var tagid = $("#tags").data('kendoComboBox').value();
                this.ajax.post('/api/v1/tag/add/' + tagid, null, el, 'Saved', () => {
                    this.reset();
                    this.renderUserTags();
                });
            });
            $('#crateAndAddTag').off('click').on('click', (el) => {
                var tagid = $("#tags").data('kendoComboBox').value();
                this.ajax.post('/api/v1/tag/create/',
                    {
                        TagName: tagid,
                        Description: $('#tagDescription').val()
                    }, el, 'Saved', () => {
                        this.reset();
                        this.renderUserTags();
                    });
            })
        }
        constructor() {
        }
    }

    export class changePassword implements settings {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.initControllers();
            this.bindViewModel();
            this.cm.bindFunctions();
        }
        private initControllers(): void {
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
        }
        private bindViewModel() {
            var viewModel = kendo.observable({
                CurrentPassword: '',
                NewPassword: '',
                ConfirmNewPassword: '',
                saved: (el) => {
                    if ($("#changepassword").data("kendoValidator").validate()) {
                        this.ajax.post('/api/v1/user/settings/ChangePassword', viewModel, el, 'Saved', (e) => {
                            viewModel.set('CurrentPassword', '');
                            viewModel.set('NewPassword', '');
                            viewModel.set('ConfirmNewPassword', '');
                        });
                    }
                }
            });
            kendo.bind($("#changepassword"), viewModel);
        }
    }
    export class previewPage implements settings {
        private ajax = new Alpha.Utility.Ajax();
        private cm = new Alpha.Utility.comman();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        public execute() {
            this.bindViewModel();
        }
        private renderUserTags() {
            $("#usertags_preview").html(Alpha.Utility.comman.loading);
            this.ajax.get('/api/v1/tag/read', null, null, '', (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#userTags-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                $("#usertags_preview").html(result);
                $('.tags').off('click').on('click', (e) => {
                    var $t = $(e.target);
                    this.cm.addRemoveTags($t.data('tagid'), $t.html(), () => {
                        this.renderUserTags();
                    });
                });
            });
        }
        private renderUserContacts() {
            $("#contactinfopreview").html(Alpha.Utility.comman.loading);
            this.ajax.get('/api/v1/user/settings/contactdetailssummery', null, null, '', (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#userContact-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                $("#contactinfopreview").html(result);
            });
        }
        private bindViewModel() {
            this.ajax.get('/api/v1/user/settings/priviewpage', null, null, '', (e) => {
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
            });
            this.renderUserTags();
            this.renderUserContacts();
        }
    }

    export class userContact implements settings {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        constructor() { }
        public execute() {
            this.renderContactTemplate();
            this.bindViewModel();
            this.cm.bindFunctions();
        }
        private renderContactTemplate() {
            $("#contactinfo").html(Alpha.Utility.comman.loading);
            this.ajax.get('/api/v1/user/settings/contactdetailssummery', null, null, '', (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#userContact-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                $("#contactinfo").html(result);
            });
        }
        private bindViewModel() {
            this.ajax.get('/api/v1/user/settings/usercontactslooksup', null, null, '', (e) => {
                var viewModel = kendo.observable({
                    Contacts: e,
                    SocialNetwork: -1,
                    Key: '',
                    Url: '',
                    IsSaveVisible: false,
                    IsRemoveVisible: false,
                    onChange: (arg) => {
                        if (viewModel.get('SocialNetwork') == -1) {
                            viewModel.set('IsSaveVisible', false);
                            viewModel.set('IsRemoveVisible', false);
                            viewModel.set('Key', '');
                            viewModel.set('Url', '');
                            return;
                        }
                        viewModel.set('IsSaveVisible', true);
                        this.ajax.get('/api/v1/user/settings/contactdetails/' + viewModel.get('SocialNetwork'), null, null, '', (e2: userContacts) => {
                            if (e2.Key == null) {
                                viewModel.set("Key", arg.sender._prev);
                                // there is no recode
                                viewModel.set('IsRemoveVisible', false);
                            } else {
                                viewModel.set('Key', e2.Key);
                                viewModel.set('IsRemoveVisible', true);
                            }
                            viewModel.set('Url', e2.Url);
                        });
                    },
                    add: (e) => {
                        this.ajax.post('/api/v1/user/settings/contactdetails', viewModel, e, 'saved', (e2: userContacts) => {
                            this.renderContactTemplate();
                            $('#contacts').animateCss(Alpha.Utility.comman.animateTypeAfterSave);
                        });
                    },
                    remove: (e) => {
                        this.ajax.post('/api/v1/user/settings/contactdetails/' + viewModel.get('SocialNetwork'), null, e, 'removed', (e2: userContacts) => {
                            this.renderContactTemplate();
                            $('#contacts').animateCss(Alpha.Utility.comman.animateTypeAfterSave);
                        });
                    }
                });
                kendo.bind($("#contacts"), viewModel);
            })
        }
    }
    export class changeProfileImage implements settings {
        constructor() { }
        public execute() {
            this.initControllers();
        }
        private initControllers() {
            $('#saveImage').off('click').on('click', () => {
                var data = new FormData();
                var d: any = $("#fileProfileImage").get(0);
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
            })
        }
    }

    export class preferences implements settings {
        private ajax = new Alpha.Utility.Ajax();
        execute() {
            this.bindViewModel();
        }
        private bindViewModel() {
            this.ajax.get('/api/v1/user/preferences', null, null, '', (e) => {
                var viewModel = kendo.observable({
                    SendNotificationEmail: e.SendNotificationEmail,
                    ShowAnonymas: e.ShowAnonymas,
                    ShowMyAsk: e.ShowMyAsk,
                    ShowMyContacts: e.ShowMyContacts,
                    ShowMyAnswers: e.ShowMyAnswers,
                    save: (el) => {
                        this.ajax.post('/api/v1/user/preferences', viewModel, el, 'Saved', () => {
                            //empty
                        });
                    }
                });
                kendo.bind($("#preferences"), viewModel);
            });
        }

        constructor() { }
    }

    export class messages implements settings {
        execute() {
            throw new Error("Method not implemented.");
        }
        constructor() { }
    }
}