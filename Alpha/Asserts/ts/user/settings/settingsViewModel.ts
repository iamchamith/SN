module Alpha.User.Settings {

    export interface user {
 
        Email : string;
        Name: string;
        Dob: Date;
        IsValiedEmail: boolean;
        Bio: string;
        Gender: number;
        //public DateTime RegDate { get; set; }
        Country: number;
        Language: number;
        MaritalStatus: number;
        ProfileImage: string;
        Employeement: string;
    }

    export interface register {
        Email: string;
        Name: string;
        Dob: Date;
        Password: string;
    }

    export interface userContacts {
        Key: string;
        Url: string;
        Id: number;
        SocialNetwork: number;
    }
}