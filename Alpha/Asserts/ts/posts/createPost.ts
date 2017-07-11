module Alpha.Post {
    enum imagetype {
        poll1, poll2, comment
    }
    export class postCreations {
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private ajax = new Alpha.Utility.Ajax();
        private viewModel = new kendo.data.ObservableObject();
        private cm = new Alpha.Utility.comman();
        constructor() { }
        public execute() {
            this.initControllers();
        }
        private genaralValidation(): boolean {
            if ($.trim(this.viewModel.get('Topic')) == '') {
                this.pop.show('please add the topic', 'info');
                $('.topic').animateCss(Alpha.Utility.comman.animateTypeAttention);
                return false;
            }
            return true;
        }
        private initControllers() {
            $('.askQuestionMenu').off('click').on('click', () => {
                this.ajax.get('/api/v1/tag/read', null, null, "", (e) => {
                    $('#model-askQuestion').modal('show').appendTo('body');
                    this.viewModel = kendo.observable({
                        Topic: '',
                        Description: '',
                        Tagss: e,
                        Tag: '',
                        IsAnonymas: false,
                        TopicLength: 150,
                        MoreLength: 300,
                        Days: 5,
                        AskTabType: 0,
                        Vs1Data: '/asserts/images/dragdrop.png',
                        Vs2Data: '/asserts/images/dragdrop.png',
                        AskCommentImage: '/asserts/images/dragdrop.png',
                        gonext: () => {
                            $('#askquestiontab .nav-tabs a[href="#menu1"]').tab('show');
                        },
                        ask: (el) => {
                            let asktype = this.viewModel.get('AskTabType');
                            let posturl = '/api/v1/post/question';
                            if (asktype == 1) {
                                posturl = '/api/v1/post/poll'
                            } else if (asktype == 2) {
                                posturl = '/api/v1/post/comment';
                            }
                            if (this.genaralValidation()) {
                                this.ajax.post(posturl, this.viewModel, el, "success share", () => {
                                    $('#model-askQuestion').modal('hide');
                                    window.location.href = '/posts/post/index?type=ask';
                                });
                            }
                        },
                        changetopic: (el) => {
                            this.viewModel.set('TopicLength', 150 - $(el.target).val().length);
                        },
                        changeMore: (el) => {
                            this.viewModel.set('MoreLength', 300 - $(el.target).val().length);
                        },
                        onDaysChange: (el) => {
                            this.viewModel.get("Days")
                        }
                    });
                    kendo.bind($("#model-askQuestion"), this.viewModel);
                });
            });
            $('#asktabtype a[data-toggle="tab"]').on('click', (e) => {
                this.viewModel.set('AskTabType', $(e.target).closest('li').index());
            });
            $('#pollImgVs1').on('change', (e: any) => {
                this.cm.fileTo64BaseString(e.target.files[0], (r) => {
                    this.viewModel.set('Vs1Data', r);
                });
            });
            $('#pollImgVs2').on('change', (e: any) => {
                this.cm.fileTo64BaseString(e.target.files[0], (r) => {
                    this.viewModel.set('Vs2Data', r);
                });
            });
            $('#askcommentImg').on('change', (e: any) => {
                this.cm.fileTo64BaseString(e.target.files[0], (r) => {
                    this.viewModel.set('AskCommentImage', r);
                });
            });
        }
    }

    $(document).ready(() => {
        let p = new Alpha.Post.postCreations();
        p.execute();
    });
}