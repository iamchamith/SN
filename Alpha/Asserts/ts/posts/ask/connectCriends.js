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
                };
                searchCriends.prototype.bindSearchData = function () {
                    var _this = this;
                    this.ajax.get('/api/v1/criends/search/looksup', null, null, function (e) {
                        _this.ajax.get('/api/v1/tag/read', null, null, function (e1) {
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
                                    _this.ajax.post('/api/v1/criends/search', search, el, function (r) {
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
                return searchCriends;
            }());
            Search.searchCriends = searchCriends;
        })(Search = Criends.Search || (Criends.Search = {}));
    })(Criends = Alpha.Criends || (Alpha.Criends = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=connectCriends.js.map