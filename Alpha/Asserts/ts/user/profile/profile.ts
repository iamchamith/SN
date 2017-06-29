module Alpha.User.Profile {

    export interface profile {
        execute();
    }

    export class answers implements profile {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        public execute() {
            this.bindViewModel();
        }
        private bindViewModel() {

        }

        constructor() { }
    }

    export class ask implements profile {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.renderUserTags();
            this.initcontrollers();
        }
        private search(q: string) {

        }
        private renderUserTags() {

        }
        private initcontrollers() {

        }
        constructor() {
        }
    }

    export class criends implements profile {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        public execute() {
            this.bindViewModel();
        }
        private bindViewModel() {

        }
    }
    export class previewPage implements profile {
        private ajax = new Alpha.Utility.Ajax();
        private cm = new Alpha.Utility.comman();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private userid: string = '';
        constructor() {
            this.userid = this.cm.getQueryString('userid');
        }
        public execute() {
            this.bindViewModel();
        }
        private renderUserTags(r) {
            $("#usertags_preview").html(Alpha.Utility.comman.loading);
            var d = [];
            d.push(r);
            var templateContent = $("#userTags-template").html();
            var template = kendo.template(templateContent);
            var result = kendo.render(template, d);
            $("#usertags_preview").html(result);
            $('.tags').off('click').on('click', (e) => {
                var $t = $(e.target);
                this.cm.addRemoveTags($t.data('tagid'), $t.html(), () => {
                });
            });
        }
        private renderUserContacts(r) {
            $("#contactinfopreview").html(Alpha.Utility.comman.loading);
            var d = [];
            d.push(r);
            var templateContent = $("#userContact-template").html();
            var template = kendo.template(templateContent);
            var result = kendo.render(template, d);
            $("#contactinfopreview").html(result);
        }
        private bindViewModel() {
            this.ajax.get(`/api/v1/userprofile/preview?guid=${this.userid}`, null, null, '', (re) => {
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
                $('#crienduserid').val(this.userid);
                $('#criendname').val(e.Name);
                $('#criendprofileimage').val(e.ProfileImage);
                this.renderUserTags(re.UserTags);
                this.renderUserContacts(re.UserContacts);
            });
        }
    }

    export class profileAskPost implements profile {
        execute() {
            this.askpost.execute();
        }
        private askpost: Alpha.Post.ask;
        constructor() {
            this.askpost = new Alpha.Post.ask();
        }
    }
    export class messages implements profile {
        private userid: string;
        private ajax = new Alpha.Utility.Ajax();
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.userid = this.cm.getQueryString('userid');
            this.bindViewModel();
            this.loadChats();
        }
        private loadChats() {
            var search = {
                ToUser: this.userid,
                Skip: 0,
                Take: 20
            }
            this.ajax.post('/api/v1/user/message/read', search, null, 'chat loaded', (e) => {
                console.log(e);
                var template = kendo.template($("#chats-template").html());
                var result = template(e);
                $("#chatContent").html(result);
            });
        }
        private bindViewModel() {
            var viewModel = kendo.observable({
                ChatWith: 'Chamith',
                Message: '',
                ProfileImage: '',
                ToUser: this.userid,
                send: (el) => {
                    this.ajax.post('/api/v1/user/message', viewModel, el, 'sent', (e) => {
                        var chat =
                            {
                                FromUser: $('#meuserid').val(),
                                ToUser: this.userid,
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
        }
        constructor() { }
    }
}