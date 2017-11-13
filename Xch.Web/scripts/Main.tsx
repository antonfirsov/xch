import * as React from "react";

import * as ReactDOM from "react-dom";

import { Ajax } from "./utils/Ajax";
import { Hello } from "./components/Hello";
import { ApiTester, RawApiTester } from "./components/ApiTester";

ReactDOM.render(
    <div>
        <Hello text="hello" />
        <RawApiTester uri="/currency/codes" />
        <RawApiTester uri="/currency/exchange?sourceAmount=1.2&sourceCurrencyCode=HUF&destCurrencyCode=EUR" />
        
    </div>
    ,
    document.getElementById("root")
);


export class Main {
    
}
