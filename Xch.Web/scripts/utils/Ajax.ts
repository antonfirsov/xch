export class Ajax {
    public static async get(url): Promise<string> {
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
            xhr.open('GET', url, true);
            xhr.send();
        });
    }
}