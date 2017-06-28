module Alpha.Utility {

    export class Ajax {
        constructor() {
            this.cm = new Alpha.Utility.comman();
        }
        private cm: Alpha.Utility.comman;
        private popupNotification = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        get(url: string, data: any, element: any = null, successmessage: string = "", callback: any) {
            if (element != null) {
                $(element.target).attr('disabled', 'disabled');
            }
            $.ajax({
                url: url, method: 'get', data: JSON.parse(data), contentType: 'application/json; charset=utf-8',
                beforeSend: () => {
                    //  this.popupNotification.show('sending...', 'info');
                }
            }).done((e) => {
                if ($.trim(successmessage) != '') {
                    this.popupNotification.show(successmessage, 'success');
                }
                callback(e);
            }).fail((e) => {
                if (e.status === 400) {
                    this.popupNotification.show(e.responseJSON.Message, 'info');
                }
                else if (e.status === 404) {
                    this.popupNotification.show('404', 'info');
                }
                else if (e.status === 401) {
                    this.popupNotification.show('Session was exprired.', 'info');
                    window.location.href = '/useracccount/authentication';
                }
                else {
                    this.popupNotification.show('server error', 'error');
                    console.error(e);
                }
            }).always(() => {
                if (element != null) {
                    $(element.target).removeAttr('disabled');
                }
            });
        }
        post(url: string, data: any, element: any = null, successmessage: string = "", callback: any) {
            if (element != null) {
                $(element.target).removeAttr('disabled');
            }
            $.ajax({
                url: url, method: 'post', data: JSON.stringify(data), contentType: 'application/json; charset=utf-8',
                beforeSend: () => {
                    //this.popupNotification.show('sending...', 'info');
                }
            }).done((e) => {
                if ($.trim(successmessage) != '') {
                    this.popupNotification.show(successmessage, 'success');
                }
                callback(e);
            }).fail((e) => {
                if (e.status === 400) {
                    this.popupNotification.show(e.responseJSON.Message, 'info');
                }
                else if (e.status === 404) {
                    this.popupNotification.show('404', 'info');
                }
                else if (e.status === 401) {
                    this.popupNotification.show('Session was exprired.', 'info');
                    window.location.href = '/useracccount/authentication';
                }
                else {
                    this.popupNotification.show('server error', 'error');
                    console.error(e);
                }
            }).always(() => {
                if (element != null) {
                    $(element.target).removeAttr('disabled');
                }
            });
        }
    }
}