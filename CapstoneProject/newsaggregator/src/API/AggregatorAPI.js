import * as ApiFetchUtility from '../Utilities/APIFetchUtility';

export async function GetAllTopics() {
    let fetchOptions = ApiFetchUtility.GetFetchOptions();
    fetchOptions['method'] = 'get';

    let url = ApiFetchUtility.getFormattedUrl("Aggregator", "GetAllTopics"); 

    const res = await ApiFetchUtility.FetchApi(url, fetchOptions);
    const json = await res.json();

    return json;
}