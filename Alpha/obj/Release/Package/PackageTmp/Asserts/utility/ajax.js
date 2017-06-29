var Alpha;
(function (Alpha) {
    var Utility;
    (function (Utility) {
        var Ajax = (function () {
            function Ajax() {
                this.popupNotification = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
                this.cm = new Alpha.Utility.comman();
            }
            Ajax.prototype.get = function (url, data, element, successmessage, callback) {
                var _this = this;
                if (element === void 0) { element = null; }
                if (successmessage === void 0) { successmessage = ""; }
                if (element != null) {
                    $(element.target).attr('disabled', 'disabled');
                }
                $.ajax({
                    url: url, method: 'get', data: JSON.parse(data), contentType: 'application/json; charset=utf-8',
                    beforeSend: function () {
                        //  this.popupNotification.show('sending...', 'info');
                    }
                }).done(function (e) {
                    if ($.trim(successmessage) != '') {
                        _this.popupNotification.show(successmessage, 'success');
                    }
                    callback(e);
                }).fail(function (e) {
                    if (e.status === 400) {
                        _this.popupNotification.show(e.responseJSON.Message, 'info');
                    }
                    else if (e.status === 404) {
                        _this.popupNotification.show('404', 'info');
                    }
                    else if (e.status === 401) {
                        _this.popupNotification.show('Session was exprired.', 'info');
                        window.location.href = '/useracccount/authentication';
                    }
                    else {
                        _this.popupNotification.show('server error', 'error');
                        console.error(e);
                    }
                }).always(function () {
                    if (element != null) {
                        $(element.target).removeAttr('disabled');
                    }
                });
            };
            Ajax.prototype.post = function (url, data, element, successmessage, callback) {
                var _this = this;
                if (element === void 0) { element = null; }
                if (successmessage === void 0) { successmessage = ""; }
                if (element != null) {
                    $(element.target).removeAttr('disabled');
                }
                $.ajax({
                    url: url, method: 'post', data: JSON.stringify(data), contentType: 'application/json; charset=utf-8',
                    beforeSend: function () {
                        //this.popupNotification.show('sending...', 'info');
                    }
                }).done(function (e) {
                    if ($.trim(successmessage) != '') {
                        _this.popupNotification.show(successmessage, 'success');
                    }
                    callback(e);
                }).fail(function (e) {
                    if (e.status === 400) {
                        _this.popupNotification.show(e.responseJSON.Message, 'info');
                    }
                    else if (e.status === 404) {
                        _this.popupNotification.show('404', 'info');
                    }
                    else if (e.status === 401) {
                        _this.popupNotification.show('Session was exprired.', 'info');
                        window.location.href = '/useracccount/authentication';
                    }
                    else {
                        _this.popupNotification.show('server error', 'error');
                        console.error(e);
                    }
                }).always(function () {
                    if (element != null) {
                        $(element.target).removeAttr('disabled');
                    }
                });
            };
            return Ajax;
        }());
        Utility.Ajax = Ajax;
    })(Utility = Alpha.Utility || (Alpha.Utility = {}));
})(Alpha || (Alpha = {}));
//# sourceMappingURL=ajax.js.map