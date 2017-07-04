/// <reference path="../../../../scripts/typings/notify.js/notify.js.d.ts" />
module Alpha.User.Authentication {
    export interface authenticate {
        execute();
    }
    export class login implements authenticate {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.validate();
            this.bindViewModel();
            this.cm.bindFunctions();
        }
        private validate(): void {
            let isvalidate: boolean;
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
        }
        private bindViewModel() {
            var viewModel = kendo.observable({
                Email: '',
                Password: '',
                login: (el) => {
                    if ($("#login").data("kendoValidator").validate()) {
                        this.ajax.post('/api/v1/auth/login', viewModel, el, 'login is success', (e) => {
                            if (e) {
                                window.location.href = "/useracccount/settings/stater#/basic";
                            } else {
                                window.location.href = "/posts/post/index?type=feed";
                            }
                        });
                    } else {
                        this.pop.showText(' Please validate the form', 'info');
                    }
                },
                openChangePasswordDialogbox: () => {
                    $('#forgetpasswordwizard .nav-tabs li:eq(0) a').tab('show');
                    $('#model-forgegtPassword').modal('show');
                    var cpw = new changePasswordRequest();
                    cpw.execute();
                }
            });
            kendo.bind($("#login"), viewModel);
        }
        constructor() {
        }
    }

    export class register implements authenticate {
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private ajax = new Alpha.Utility.Ajax();
        private cm = new Alpha.Utility.comman();
        public execute() {
            this.validate();
            this.bindViewModel();
            this.cm.bindFunctions();
        }
        private validate(): void {
            let isvalidate: boolean;
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
        }
        private bindViewModel() {
            var viewModel = kendo.observable({
                Email: '',
                Password: '',
                Name: '',
                register: (el) => {
                    if ($("#register").data("kendoValidator").validate()) {
                        this.pop.show(' sending request...', 'info');
                        this.ajax.post('/api/v1/auth/register', viewModel, el, 'registration success', (e) => {
                            window.location.href = "/useracccount/settings/stater#/basic";
                        });
                    } else {
                        this.pop.showText(' Please validate the form', 'info');
                    }
                }
            });
            kendo.bind($("#register"), viewModel);
        }
        constructor() {
        }
    }

    export class changePasswordRequest implements authenticate {
        private ajax = new Alpha.Utility.Ajax();
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        public execute() {
            this.validate();
            this.initContollers();
            this.bindViewModel();
        }
        private validate(): void {
            let isvalidate: boolean;
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
        }
        private initContollers(): void {
            $("#model-forgegtPassword .nav-tabs a[data-toggle=tab]").on("click", function (e) {
                let pop2 = $("#notification").kendoNotification().data("kendoNotification");
                pop2.show('please enter email', 'info');
                e.preventDefault();
                return false;
            });
        }
        private bindViewModel() {
            var viewModel = kendo.observable({
                Email: '',
                NewPassword: '',
                ConfirmNewPassword: '',
                Token: '',
                sendForgetPasswordRecoveryRequest: (el) => {
                    if ($("#model-forgegtPassword").data("kendoValidator").validate()) {
                        this.pop.show('wait for sending email', 'info');
                        this.ajax.get('/api/v1/auth/forgetpasswordrequest?email=' + viewModel.get("Email"), null, el, 'email sent.please type the token', (e) => {
                            $('#model-forgegtPassword .nav-tabs li:eq(1) a').tab('show');
                            this.pop.hide();
                        });
                    }
                },
                validateForgetPasswordToken: (el) => {
                    this.pop.show('wait for validate forget password token', 'info');
                    this.ajax.get('/api/v1/auth/forgetpasswordrequesttokenvalidate?email=' + viewModel.get("Email") + '&token=' + viewModel.get("Token"),
                        null, el, 'token validation success.please enter new password', (e) => {
                            $('#model-forgegtPassword .nav-tabs li:eq(2) a').tab('show');
                            this.pop.hide();
                        });
                },
                changePassword: (el) => {
                    this.ajax.post('/api/v1/auth/changepassword', viewModel, el, '', (e) => {
                        window.location.href = '/useracccount/settings';
                    });
                },
                backToSendEmail: () => {
                    $('#model-forgegtPassword .nav-tabs li:eq(0) a').tab('show');
                },
                skip: () => {
                    window.location.href = '/useracccount/settings?type=start#/basic';
                }
            });
            kendo.bind($("#model-forgegtPassword"), viewModel);
        }
        constructor() { }

    }
}