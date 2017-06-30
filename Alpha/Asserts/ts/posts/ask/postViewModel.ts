module Alpha.Post {

    export interface postSearchRequest {
        UserId: string;
        Topic: string;
        IsDateDesc: boolean;
        Tags: number[];
        IsQuestions: boolean;
        IsPoll: boolean;
        IsNeedComments: boolean;
        IsMyAsks: boolean;
        IsMyAnswers: boolean;
        Skip: number;
        Take: number;
    }
}