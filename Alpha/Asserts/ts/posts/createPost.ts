module Alpha.Post {
    enum imagetype {
        poll1, poll2, comment
    }
    export class postCreations {
        private pop = $("#notification").kendoNotification().data("kendoNotification");
        private ajax = new Alpha.Utility.Ajax();
        private viewModel = new kendo.data.ObservableObject();
        constructor() { }
        public execute() {
            this.initControllers();
        }

        private initControllers() {
            function getBase64(file, callback) {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {
                    callback(reader.result);
                };
                reader.onerror = function (error) {
                    console.log('Error: ', error);
                };
            }
            $('.askQuestionMenu').off('click').on('click', () => {
                this.ajax.get('/api/v1/tag/read', null, null, "", (e) => {
                    $('#model-askQuestion').modal('show').appendTo('body');
                    this.viewModel = kendo.observable({
                        Topic: '',
                        Description: '',
                        Tagss: e,
                        Tag: '',
                        IsAnonymas: false,
                        Vs1Data: '/asserts/images/dragdrop.png',
                        Vs2Data: '/asserts/images/dragdrop.png',
                        AskCommentImage: '/asserts/images/dragdrop.png',
                        askcomment: (el) => {
                            this.ajax.post('/api/v1/post/comment', this.viewModel, el,"success share", () => {
                                $('#model-askQuestion').modal('hide');
                            });
                        },
                        askpoll: (el) => {
                            this.ajax.post('/api/v1/post/poll', this.viewModel, el,"success share", () => {
                                $('#model-askQuestion').modal('hide');
                            });
                        },
                        askquestion: (el) => {
                            this.ajax.post('/api/v1/post/question', this.viewModel, el,"success share", () => {
                                $('#model-askQuestion').modal('hide');
                            });
                        }
                    });
                    kendo.bind($("#model-askQuestion"), this.viewModel);
                });

            });
            $('#pollImgVs1').on('change', (e: any) => {
                getBase64(e.target.files[0], (r) => {
                    this.viewModel.set('Vs1Data', r);
                });
            });
            $('#pollImgVs2').on('change', (e: any) => {
                getBase64(e.target.files[0], (r) => {
                    this.viewModel.set('Vs2Data', r);
                });
            });
            $('#askcommentImg').on('change', (e: any) => {
                getBase64(e.target.files[0], (r) => {
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