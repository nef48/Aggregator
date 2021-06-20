import { IsNullOrUndefined } from "./CommonUtilities";

export function GetFetchOptions(): RequestInit {
    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'X-Requested-With': 'XMLHttpRequest',
        'Access-Control-Allow-Origin': '*'
    };

    //let fetchCredentialsElement: HTMLInputElement = document.getElementById("fetchCredentialsDefault") as HTMLInputElement;
    //let fetchCredentialsValue: RequestCredentials = 'same-origin';
    let fetchModeElement: HTMLInputElement = document.getElementById("fetchModeDefault");
    let fetchModeValue: RequestMode = 'same-origin';

    // if (!IsNullOrUndefined(fetchCredentialsElement) && fetchCredentialsElement.value !== "") {
    //     fetchCredentialsValue = fetchCredentialsElement.value as RequestCredentials;
    // }

    if (!IsNullOrUndefined(fetchModeElement) && fetchModeElement.value !== "") {
        fetchModeValue = fetchModeElement.value;
    }
    
    return {
        headers: headers,
        // credentials: fetchCredentialsValue,
        credentials: 'omit',
        mode: fetchModeValue
    };
}

export function getFormattedUrl(controllerName: string, methodName: string): string {
    let requestUrlElement: HTMLInputElement = document.getElementById("requestUrl"); 
    let requestUrl: string = "";

    if (!IsNullOrUndefined(requestUrlElement)) {
        requestUrl = requestUrlElement.value;
    }
    else {
        //let baseUrlElement: HTMLInputElement = document.getElementById("baseUrl") as HTMLInputElement; 
        requestUrl = "http://localhost:44393";
    }

    return requestUrl + "/" + controllerName + "/" + methodName;
}

export async function FetchApi(url: string, options?: any): Promise<Response> {
    try {
        console.dir(options);
        const res = await fetch(url, options);
        if (res.status === 401) {
            window.location.reload(true);
        }
        return res;
    } catch (err) {
        throw err;
    }
}