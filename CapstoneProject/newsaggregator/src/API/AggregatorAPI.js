import { Topic } from '../Classes/Topic';
import * as ApiFetchUtility from '../Utilities/APIFetchUtility';

export async function GetAllTopics() {
    let fetchOptions = ApiFetchUtility.GetFetchOptions();
    fetchOptions['method'] = 'get';

    let url = new URL(ApiFetchUtility.getFormattedUrl("Aggregator", "GetAllTopics"));

    const res = await ApiFetchUtility.FetchApi(url, fetchOptions);
    const json = await res.json();

    return json;
}

export async function GetNewsFeed(searchTopic: string) {
    let fetchOptions = ApiFetchUtility.GetFetchOptions();
    fetchOptions['method'] = 'post';
    let params = {topic: searchTopic}

    let url = new URL(ApiFetchUtility.getFormattedUrl("Aggregator", "GetArticles"));

    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    const res = await ApiFetchUtility.FetchApi(url, fetchOptions);
    const json = await res.json();

    return json;
}