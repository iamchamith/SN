var Aplha;
(function (Aplha) {
    var User;
    (function (User) {
        var Authentication;
        (function (Authentication) {
            var login = (function () {
                function login() {
                }
                return login;
            }());
            Authentication.login = login;
            var register = (function () {
                function register() {
                }
                return register;
            }());
            Authentication.register = register;
            var changePasswordRequest = (function () {
                function changePasswordRequest() {
                }
                return changePasswordRequest;
            }());
            Authentication.changePasswordRequest = changePasswordRequest;
        })(Authentication = User.Authentication || (User.Authentication = {}));
    })(User = Aplha.User || (Aplha.User = {}));
})(Aplha || (Aplha = {}));
//# sourceMappingURL=authontication.js.map