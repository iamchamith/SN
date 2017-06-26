module Alpha.Post {

    export interface postSearchRequest {
        UserId: string;
        Topic: string;
        IsDateDesc: boolean;
        Tags: number[];
    }
}