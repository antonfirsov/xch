import * as React from "react";

import * as ReactDOM from "react-dom";

import { Ajax } from "./utils/Ajax";
import { Hello } from "./components/Hello";
import { ApiTester, RawApiTester, CurrencyConverterApiTester } from "./components/ApiTester";

import { CurrencyPicker } from "./components/CurrencyPicker";
import { CurrencyConverter } from "./components/CurrencyConverter";

//import * as DropdownButton from "react-bootstrap/lib/DropdownButton";
//import * as MenuItem from "react-bootstrap/lib/MenuItem";
import { DropdownButton, MenuItem} from "react-bootstrap";

ReactDOM.render(
    <div>
        <Hello text="hello" />

        <CurrencyPicker uri="/currency/codes" />
        <CurrencyPicker codes={['foo', 'bar']} onChanged={s => alert(s)} defaultSelected='bar' />
        <CurrencyConverterApiTester uri="/currency/exchange" />
        <CurrencyConverter codesUri="/currency/codes" converterUri="/currency/exchange" />
    </div>
    ,
    document.getElementById("root")
);

//<RawApiTester uri="/currency/codes" />
//<RawApiTester uri="/currency/exchange?sourceAmount=1.2&sourceCurrencyCode=HUF&destCurrencyCode=EUR" />

export class Main {
    
}
