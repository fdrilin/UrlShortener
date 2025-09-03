import { Enums } from "./Enums.js"

function getUrl(name) {
    return "https://localhost:7269/api/" + name;
}

function getToken()
{
    let currentUser = sessionStorage.getItem(Enums.currentUser);
    if (currentUser) {
        return "Bearer " + currentUser;
    }
    else {
        return "";
    }
}

export function simpleFetch(type, location, keys, values, successAction, failureAction) {
    let body = {};
    let options = {
        method: type,
        crossorigin: true,
        headers:
        {
            'Content-Type': 'application/json',
            "Authorization": getToken()
        }
    }

    keys.forEach((item, i) => {
        body[item] = values[i]
    });
    if (Object.keys(body).length != 0) {
        body = JSON.stringify(body);
        options["body"] = body;
    }

    fetch(getUrl(location), options)
        .then(res => {
            let data = res.json();
            console.log(res);
            if (res.ok) {
                data.then(res => {
                    successAction(res);
                });
            } else {
                data.then(res => {
                    failureAction(res.message)
                });
            }
        })
        .catch(_ => console.error(_));
}