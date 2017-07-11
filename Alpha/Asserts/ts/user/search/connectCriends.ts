module Alpha.Criends.Search {
    export interface search {
        execute();
    }
    export class searchCriends implements search {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        private mvvm = new kendo.data.ObservableObject();
        constructor() { }
        public execute() {
            this.initControllers();
            this.bindSearchData();
        }
        private initControllers() {
            $('#resetSearch').off('click').on('click', () => {
                this.bindSearchData();
            });
            $('#criendsloadmore').off('click').on('click', (el) => {
                var search = {
                    Name: this.mvvm.get('Name'),
                    Country: (this.mvvm.get('Country') == null) ? -1 : this.mvvm.get('Country'),
                    Sex: (this.mvvm.get('Gender') == null) ? -1 : this.mvvm.get('Gender'),
                    MaritalStatus: (this.mvvm.get('MaritalStatus') == null) ? -1 : this.mvvm.get('MaritalStatus'),
                    Skip: Number($('#criendsearchskip').val()),
                    Take: 10
                };
                this.searchCriends(search, el, true);
                $('#criendsearchskip').val(Number($('#criendsearchskip').val()) + 10);
            });
            $('.relation').off('click').on('click', (el) => {
                let searchRequest = {
                    OparationType: !$(el.target).data('isrelation'),
                    UserId: $(el.target).data('userid'),
                    State: $(el.target).data('type')
                };
                this.cm.sendRelationshipRequest(searchRequest, el);
            });
            $('.tags').off('click').on('click', (e) => {
                var $t = $(e.target);
                this.cm.addRemoveTags($t.data('tagid'), $t.html(), () => {
                });
            });
        }

        private bindSearchData() {
            this.ajax.get('/api/v1/criends/search/looksup', null, null, "", (e) => {
                this.ajax.get('/api/v1/tag/read', null, null, "", (e1) => {
                    this.mvvm = kendo.observable({
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
                                Name: this.mvvm.get('Name'),
                                Country: (this.mvvm.get('Country') == null) ? -1 : this.mvvm.get('Country'),
                                Sex: (this.mvvm.get('Gender') == null) ? -1 : this.mvvm.get('Gender'),
                                MaritalStatus: (this.mvvm.get('MaritalStatus') == null) ? -1 : this.mvvm.get('MaritalStatus'),
                                Skip: 0,
                                Take: 10
                            };
                            this.searchCriends(search, el);
                            $('#criendsearchskip').val(10);
                        },
                        any: (el) => {
                            $("#searchcriends").html(Alpha.Utility.comman.loading);
                            var search = {
                                Name: '',
                                Country: -1,
                                Sex: -1,
                                MaritalStatus: -1,
                                Skip: 0,
                                Take: 10
                            };
                            this.searchCriends(search, el);
                            $('#criendsearchskip').val(10);
                        }
                    });
                    kendo.bind($("#criendsearch"), this.mvvm);
                    var search = {
                        Name: this.mvvm.get('Name'),
                        Country: (this.mvvm.get('Country') == null) ? -1 : this.mvvm.get('Country'),
                        Sex: (this.mvvm.get('Gender') == null) ? -1 : this.mvvm.get('Gender'),
                        MaritalStatus: (this.mvvm.get('MaritalStatus') == null) ? -1 : this.mvvm.get('MaritalStatus'),
                        Skip: 0,
                        Take: 10
                    };
                    this.searchCriends(search, null);
                    $('#criendsearchskip').val(10);
                });
            });
        }
        private searchCriends(search, el, isappend: boolean = false) {
            let $searchmore = $('#criendsloadmore');
            if ($searchmore.hasClass('hidden')) {
                $searchmore.removeClass('hidden');
            }
            this.ajax.post('/api/v1/criends/search', search, el, "", (r) => {
                var d = [];
                d.push(r);
                var templateContent = $("#searchCriends-template").html();
                var template = kendo.template(templateContent);
                var result = kendo.render(template, d);
                if (!isappend) {
                    $("#searchcriends").html(result);
                    $('#searchCount').html(r.length);
                    $('#searchcriendplaceholder').animateCss(Alpha.Utility.comman.animateType);
                } else {
                    $("#searchcriends").append(result);
                    $('#searchCount').html(Number($('#searchCount').html()) + r.length);
                }
                if (r.length == 0) {
                    $searchmore.addClass('hidden');
                }
                this.initControllers();
            });
        }
    }

    export class relationshipCriends implements search {
        execute() {
            throw new Error("Method not implemented.");
        }

    }
}