export class Ajax {
    public static async getRaw(uri:string): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            var xhr = new XMLHttpRequest();

            xhr.onreadystatechange = event => {
                if (xhr.readyState !== 4) return;
                if (xhr.status >= 200 && xhr.status < 300) {
                    resolve(xhr.responseText);
                } else {
                    reject(xhr.statusText);
                }
            };
            xhr.open('GET', uri, true);
            xhr.send();
        });
    }

    public static async get<T>(uri: string, parameters: any = null): Promise<T> {
        if (parameters) {
            var str = "";
            for (var key in parameters) {
                if (str != "") {
                    str += "&";
                }
                str += key + "=" + encodeURIComponent(parameters[key]);
            }
            uri = uri + '?' + str;
        }
        var result = await Ajax.getRaw(uri);
        var wut = JSON.parse(result);
        return <T>wut;
    }
}

