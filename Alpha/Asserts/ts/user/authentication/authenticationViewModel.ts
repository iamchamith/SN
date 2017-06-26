module Alpha.User.Authentication {

    export interface login {

    }

    export interface register {
        Email: string;
        Name: string;
        Dob: Date;
        Password: string;
    }

    export interface forgetPasswordRequest {

    }
}