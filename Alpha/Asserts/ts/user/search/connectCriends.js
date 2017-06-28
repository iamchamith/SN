var Alpha;
(function (Alpha) {
    var Criends;
    (function (Criends) {
        var Search;
        (function (Search) {
            var searchCriends = (function () {
                function searchCriends() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                    this.mvvm = new kendo.data.ObservableObject();
                }
                searchCriends.prototype.execute = function () {
                    this.initControllers();
                    this.bindSearchData();
                };
                searchCriends.prototype.initControllers = function () {
                    var _this = this;
                    $('#resetSearch').off('click').on('click', function () {
                        _this.bindSearchData();
                    });
                    $('#criendsloadmore').off('click').on('click', function (el) {
                        var search = {
                            Name: _this.mvvm.get('Name'),
                            Country: (_this.mvvm.get('Country') == null) ? -1 : _this.mvvm.get('Country'),
                            Sex: (_this.mvvm.get('Gender') == null) ? -1 : _this.mvvm.get('Gender'),
                            MaritalStatus: (_this.mvvm.get('MaritalStatus') == null) ? -1 : _this.mvvm.get('MaritalStatus'),
                            Skip: Number($('#criendsearchskip').val()),
                            Take: 10
                        };
                        _this.searchCriends(search, el, true);
                        $('#criendsearchskip').val(Number($('#criendsearchskip').val()) + 10);
                    });
                    $('.relation').off('click').on('click', function (el) {
                        var searchRequest = {
                            OparationType: !$(el.target).data('isrelation'),
                            UserId: $(el.target).data('userid'),
                            State: $(el.target).data('type')
                        };
                        _this.cm.sendRelationshipRequest(searchRequest, el);
                    });
                    $('.tags').off('click').on('click', function (e) {
                        var $t = $(e.target);
                        _this.cm.addRemoveTags($t.data('tagid'), $t.html(), function () {
                        });
                    });
                };
                searchCriends.prototype.bindSearchData = function () {
                    var _this = this;
                    this.ajax.get('/api/v1/criends/search/looksup', null, null, "", function (e) {
                        _this.ajax.get('/api/v1/tag/read', null, null, "", function (e1) {
                            _this.mvvm = kendo.observable({
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
                                        Name: _this.mvvm.get('Name'),
                                        Country: (_this.mvvm.get('Country') == null) ? -1 : _this.mvvm.get('Country'),
                                        Sex: (_this.mvvm.get('Gender') == null) ? -1 : _this.mvvm.get('Gender'),
                                        MaritalStatus: (_this.mvvm.get('MaritalStatus') == null) ? -1 : _this.mvvm.get('MaritalStatus'),
                                        Skip: 0,
                                        Take: 10
                                    };
                                    _this.searchCriends(search, el);
                                    $('#criendsearchskip').val(10);
                                }
                            });
                            kendo.bind($("#criendsearch"), _this.mvvm);
                            var search = {
                                Name: _this.mvvm.get('Name'),
                                Country: (_this.mvvm.get('Country') == null) ? -1 : _this.mvvm.get('Country'),
                                Sex: (_this.mvvm.get('Gender') == null) ? -1 : _this.mvvm.get('Gender'),
                                MaritalStatus: (_this.mvvm.get('MaritalStatus') == null) ? -1 : _this.mvvm.get('MaritalStatus'),
                                Skip: 0,
                                Take: 10
                            };
                            _this.searchCriends(search, null);
                            $('#criendsearchskip').val(10);
                        });
                    });
                };
                searchCriends.prototype.searchCriends = function (search, el, isappend) {
                    var _this = this;
                    if (isappend === void 0) { isappend = false; }
                    var $searchmore = $('#criendsloadmore');
                    if ($searchmore.hasClass('hidden')) {
                        $searchmore.removeClass('hidden');
                    }
                    this.ajax.post('/api/v1/criends/search', search, el, "", function (r) {
                        var d = [];
                        d.push(r);
                        var templateContent = $("#searchCriends-template").html();
                        var template = kendo.template(templateContent);
                        var result = kendo.render(template, d);
                        if (!isappend) {
                            $("#searchcriends").html(result);
                            $('#searchCount').html(r.length);
                            $('#searchcriendplaceholder').animateCss(Alpha.Utility.comman.animateType);
                        }
                        else {
                            $("#searchcriends").append(result);
                            $('#searchCount').html(Number($('#searchCount').html()) + r.length);
                        }
                        if (r.length == 0) {
                            $searchmore.addClass('hidden');
                        }
                        _this.initControllers();
                    });
                };
                return searchCriends;
            }());
            Search.searchCriends = searchCriends;
            var relationshipCriends = (function () {
                function relationshipCriends() {
                }
                relationshipCriends.prototype.execute = function () {
                    throw new Error("Method not implemented.");
                };
                return relationshipCriends;
            }());
            Search.relationshipCriends = relationshipCriends;
        })(Search = Criends.Search || (Criends.Search = {}));
    })(Criends = Alpha.Criends || (Alpha.Criends = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=connectCriends.js.map