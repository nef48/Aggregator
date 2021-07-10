export class Article {
    ArticleAuthor: string;
    ArticleDescription: string;
    ArticleLink: string;
    DatePublished: Date;
    ArticleTitle: string;
    AdditionalDescription: string;

    constructor() {
        this.ArticleAuthor = "";
        this.ArticleDescription = "";
        this.ArticleLink = "";
        this.DatePublished = null;
        this.ArticleTitle = "";
        this.AdditionalDescription = "";
    }
}