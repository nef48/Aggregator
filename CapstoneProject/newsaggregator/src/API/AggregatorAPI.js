import { Topic } from '../Classes/Topic';
import * as ApiFetchUtility from '../Utilities/APIFetchUtility';

export async function GetAllTopics() {
    let fetchOptions = ApiFetchUtility.GetFetchOptions();
    fetchOptions['method'] = 'get';

    let url = ApiFetchUtility.getFormattedUrl("Aggregator", "GetAllTopics"); 

    const res = await ApiFetchUtility.FetchApi(url, fetchOptions);
    const json = await res.json();

    return json;
}

export async function GetNewsFeed(topic: Topic) {
    let fetchOptions = ApiFetchUtility.GetFetchOptions();
    fetchOptions['method'] = 'get';
    fetchOptions['body'] = JSON.stringify({ "topic" : topic });

    let url = ApiFetchUtility.getFormattedUrl("Aggregator", "GetArticles");

    const res = await ApiFetchUtility.FetchApi(url, fetchOptions);
    const json = await res.json();

    return json;
}