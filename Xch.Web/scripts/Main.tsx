import * as React from "react";

import * as ReactDOM from "react-dom";

import { Hello } from "./components/Hello";
import { ApiTester} from "./components/ApiTester";

//Hello.renderToElement("root");

ReactDOM.render(
    <div>
        <Hello text="hello" />

        <ApiTester uri="/currency/codes" />
        <ApiTester uri="/currency/exchange?sourceAmount=1.2&sourceCurrencyCode=HUF&destCurrencyCode=EUR" />
    </div>
    ,
    document.getElementById("root")
);


export class Main {
    
}
