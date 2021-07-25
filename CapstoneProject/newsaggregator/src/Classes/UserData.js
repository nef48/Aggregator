export class UserData {
    UserID: number;
    Username: string;
    Password: string;
    DateCreated: Date;
    LastLogin: Date;

    constructor() {
        this.UserID = 0;
        this.Username = "";
        this.Password = "";
        this.DateCreated = null;
        this.LastLogin = null;
    }
}