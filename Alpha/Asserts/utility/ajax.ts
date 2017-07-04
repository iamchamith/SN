module Alpha.Utility {

    export class Ajax {
        constructor() {
            this.cm = new Alpha.Utility.comman();
        }
        private cm: Alpha.Utility.comman;
        private popupNotification = $("#notification").kendoNotification({ position: { top: 0, bottom: 20, right: 10 } }).data("kendoNotification");
        get(url: string, data: any, element: any = null, successmessage: string = "", callback: any) {
            let $el;
            $.ajax({
                url: url, method: 'get', data: JSON.parse(data), contentType: 'application/json; charset=utf-8',
                beforeSend: () => {
                    if (element != null) {
                        $el = $(element.target);
                        $el.children('.d').addClass('hidden');
                        $el.prepend('<i class="fa fa-spinner x" aria-hidden="true"></i>').attr('disabled', 'disabled');
                    }
                },
                complete: () => {

                }
            }).done((e) => {
                if (element != null) {
                    $el.removeAttr('disabled');
                    $el.children('.x').remove();
                    $el.children('.d').removeClass('hidden');
                }
                if ($.trim(successmessage) != '') {
                    this.popupNotification.show(successmessage, 'success');
                }
                callback(e);
            }).fail((e) => {
                if (element != null) {
                    $el.removeAttr('disabled');
                    $el.children('.x').remove();
                    $el.children('.d').removeClass('hidden');
                }
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
                    $(element.target).children('.x').remove();
                }
            });
        }
        post(url: string, data: any, element: any = null, successmessage: string = "", callback: any) {
            let $el;
            $.ajax({
                url: url, method: 'post', data: JSON.stringify(data), contentType: 'application/json; charset=utf-8',
                beforeSend: () => {
                    if (element != null) {
                        $el = $(element.target);
                        $el.children('.d').addClass('hidden');
                        $el.prepend('<i class="fa fa-spinner x" aria-hidden="true"></i>').attr('disabled', 'disabled');
                    }
                },
                complete: () => {

                }
            }).done((e) => {
                if (element != null) {
                    $el.removeAttr('disabled');
                    $el.children('.x').remove();
                    $el.children('.d').removeClass('hidden');
                }
                if ($.trim(successmessage) != '') {
                    this.popupNotification.show(successmessage, 'success');
                }
                callback(e);
            }).fail((e) => {
                if (element != null) {
                    $el.removeAttr('disabled');
                    $el.children('.x').remove();
                    $el.children('.d').removeClass('hidden');
                }
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

            });
        }
    }
}