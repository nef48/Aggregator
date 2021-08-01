import { Topic } from "./Topic";
import { UserData } from "./UserData";

export class LoginObject {
    User: UserData;
    Topics: Topic[];

    constructor() {
        this.User = new UserData();
        this.Topics = [];
    }
}