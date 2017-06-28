/// <reference path="../../../../scripts/typings/notify.js/notify.js.d.ts" />
var Alpha;
(function (Alpha) {
    var User;
    (function (User) {
        var Authentication;
        (function (Authentication) {
            var login = (function () {
                function login() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.popupNotification = $("#notification").kendoNotification().data("kendoNotification");
                    this.cm = new Alpha.Utility.comman();
                }
                login.prototype.execute = function () {
                    this.validate();
                    this.bindViewModel();
                    this.cm.bindFunctions();
                };
                login.prototype.validate = function () {
                    var isvalidate;
                    $("#login").kendoValidator({
                        rules: {
                            customRule1: function (input) {
                                if (input.is("[name=Email]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule2: function (input) {
                                if (input.is("[name=Password]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                        },
                        messages: {
                            customRule1: "Email requred",
                            customRule2: "password requred"
                        }
                    });
                };
                login.prototype.bindViewModel = function () {
                    var _this = this;
                    var viewModel = kendo.observable({
                        Email: '',
                        Password: '',
                        login: function (el) {
                            if ($("#login").data("kendoValidator").validate()) {
                                _this.ajax.post('/api/v1/auth/login', viewModel, el, 'login is success', function (e) {
                                    window.location.href = "/useracccount/settings";
                                });
                            }
                        },
                        openChangePasswordDialogbox: function () {
                            $('.nav-tabs li:eq(0) a').tab('show');
                            $('#model-forgegtPassword').modal('show');
                            var cpw = new changePasswordRequest();
                            cpw.execute();
                        }
                    });
                    kendo.bind($("#login"), viewModel);
                };
                return login;
            }());
            Authentication.login = login;
            var register = (function () {
                function register() {
                    this.popupNotification = $("#notification").kendoNotification().data("kendoNotification");
                    this.ajax = new Alpha.Utility.Ajax();
                    this.cm = new Alpha.Utility.comman();
                }
                register.prototype.execute = function () {
                    this.validate();
                    this.bindViewModel();
                    this.cm.bindFunctions();
                };
                register.prototype.validate = function () {
                    var isvalidate;
                    $("#register").kendoValidator({
                        rules: {
                            customRule1: function (input) {
                                if (input.is("[name=regEmail]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule2: function (input) {
                                if (input.is("[name=regName]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule3: function (input) {
                                if (input.is("[name=regPassword]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                        },
                        messages: {
                            customRule1: "Email requred",
                            customRule2: "Name requred",
                            customRule3: "password requred"
                        }
                    });
                };
                register.prototype.bindViewModel = function () {
                    var _this = this;
                    var viewModel = kendo.observable({
                        Email: '',
                        Password: '',
                        Name: '',
                        register: function (el) {
                            if ($("#register").data("kendoValidator").validate()) {
                                _this.popupNotification.show(' sending request...', 'info');
                                _this.ajax.post('/api/v1/auth/register', viewModel, el, 'registration success', function (e) {
                                    window.location.href = "/useracccount/settings";
                                });
                            }
                        }
                    });
                    kendo.bind($("#register"), viewModel);
                };
                return register;
            }());
            Authentication.register = register;
            var changePasswordRequest = (function () {
                function changePasswordRequest() {
                    this.ajax = new Alpha.Utility.Ajax();
                    this.pop = $("#notification").kendoNotification().data("kendoNotification");
                }
                changePasswordRequest.prototype.execute = function () {
                    this.validate();
                    this.initContollers();
                    this.bindViewModel();
                };
                changePasswordRequest.prototype.validate = function () {
                    var isvalidate;
                    $("#model-forgegtPassword").kendoValidator({
                        rules: {
                            customRule1: function (input) {
                                if (input.is("[name=Email]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                            customRule2: function (input) {
                                if (input.is("[name=Password]")) {
                                    return $.trim(input.val()) !== "";
                                }
                                return true;
                            },
                        },
                        messages: {
                            customRule1: "Email requred",
                            customRule2: "password requred"
                        }
                    });
                };
                changePasswordRequest.prototype.initContollers = function () {
                    $(".nav-tabs a[data-toggle=tab]").on("click", function (e) {
                        var pop2 = $("#notification").kendoNotification().data("kendoNotification");
                        pop2.show('please enter email', 'info');
                        e.preventDefault();
                        return false;
                    });
                };
                changePasswordRequest.prototype.bindViewModel = function () {
                    var _this = this;
                    var viewModel = kendo.observable({
                        Email: '',
                        NewPassword: '',
                        ConfirmNewPassword: '',
                        Token: '',
                        sendForgetPasswordRecoveryRequest: function (el) {
                            if ($("#model-forgegtPassword").data("kendoValidator").validate()) {
                                _this.pop.show('wait for sending email', 'info');
                                _this.ajax.get('/api/v1/auth/forgetpasswordrequest?email=' + viewModel.get("Email"), null, el, 'email sent.please type the token', function (e) {
                                    $('.nav-tabs li:eq(1) a').tab('show');
                                    _this.pop.hide();
                                });
                            }
                        },
                        validateForgetPasswordToken: function (el) {
                            _this.pop.show('wait for validate forget password token', 'info');
                            _this.ajax.get('/api/v1/auth/forgetpasswordrequesttokenvalidate?email=' + viewModel.get("Email") + '&token=' + viewModel.get("Token"), null, el, 'token validation success.please enter new password', function (e) {
                                $('.nav-tabs li:eq(2) a').tab('show');
                                _this.pop.hide();
                            });
                        },
                        changePassword: function (el) {
                            _this.ajax.post('/api/v1/auth/changepassword', viewModel, el, '', function (e) {
                                window.location.href = '/useracccount/settings';
                            });
                        },
                        backToSendEmail: function () {
                            $('.nav-tabs li:eq(0) a').tab('show');
                        },
                        skip: function () {
                            window.location.href = '/useracccount/settings';
                        }
                    });
                    kendo.bind($("#model-forgegtPassword"), viewModel);
                };
                return changePasswordRequest;
            }());
            Authentication.changePasswordRequest = changePasswordRequest;
        })(Authentication = User.Authentication || (User.Authentication = {}));
    })(User = Alpha.User || (Alpha.User = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=authentication.js.map