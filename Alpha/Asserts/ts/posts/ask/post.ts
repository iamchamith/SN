/// <reference path="postviewmodel.ts" />
module Alpha.Post {
    export interface post {
        execute();
    }
    export class ask implements post {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        private userid;
        private cm = new Alpha.Utility.comman();
        constructor() {
            this.userid = this.cm.getQueryString('userid');
        }
        public execute() {
            this.initController();
            this.bindSearchViewModel();
            var search: postSearchRequest = {
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
        }
        private bindSearchViewModel() {
            this.ajax.get('/api/v1/tag/read', null, null, "", (e) => {
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
                    search: (el) => {
                        var search: postSearchRequest = {
                            Topic: viewModel.get('Titile'),
                            IsDateDesc: viewModel.get('Reason'),
                            UserId: this.userid,
                            Skip: 0,
                            Take: 10,
                            IsMyAnswers: viewModel.get('IsMyAnswers'),
                            IsMyAsks: viewModel.get('IsMyAsks'),
                            IsNeedComments: viewModel.get('IsNeedComments'),
                            IsPoll: viewModel.get('IsPoll'),
                            IsQuestions: viewModel.get('IsQuestions'),
                            Tags: []
                        };
                        this.bindSearchData(search);
                    }, reset: () => {
                        var search: postSearchRequest = {
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
                        viewModel.set('Titile', '');
                        viewModel.set('Reason', true);
                    }
                });
                kendo.bind($("#searchpostcontrollers"), viewModel);
            });
        }
        private initController() {
            $('.like').off('click').on('click', () => { alert('liked'); });
            $('.dislike').off('click').on('click', () => { alert('liked'); });
            $('.viewpost').off('click').on('click', () => {
                $('#showpostinfo-model').modal('show');
            });
        }
        private bindSearchData(e: postSearchRequest) {
            this.ajax.post('/api/v1/post/search', e, null, "search is complete", (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#posts-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                $("#searchposts").html(result);
                $('#seachpostresult').animateCss(Alpha.Utility.comman.animateTypeAfterSearch);
                this.initController();
            });
        }
    }

    export class answers implements post {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        constructor() { }
        public execute() {
            this.initControllers();
            this.bindSearchData();
        }
        private initControllers() {
            $('#resetSearch').off('click').on('click', () => {
                this.bindSearchData();
            });
        }
        private bindSearchData() {
            this.ajax.get('/api/v1/criends/search/looksup', null, null, "", (e) => {
                this.ajax.get('/api/v1/tag/read', null, null, "", (e1) => {
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
                        Search: (el) => {
                            $("#searchcriends").html(Alpha.Utility.comman.loading);
                            var search = {
                                Name: viewModel.get('Name'),
                                Country: viewModel.get('Country'),
                                Sex: viewModel.get('Gender'),
                                MaritalStatus: viewModel.get('MaritalStatus'),
                            };
                            this.ajax.post('/api/v1/criends/search', search, el, "", (r) => {
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
        }
    }
}