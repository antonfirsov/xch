import * as React from "react";
import * as ReactDOM from "react-dom";

import { Ajax } from "./utils/Ajax";
import { Hello } from "./components/Hello";
import { ApiTester, RawApiTester, CurrencyConverterApiTester, CurrencyHistoryApiTester } from "./components/ApiTester";

import { CurrencyPicker } from "./components/CurrencyPicker";
import { CurrencyConverter } from "./components/CurrencyConverter";
import { CurrencyHistoryPresenter } from "./components/CurrencyHistoryPresenter"

ReactDOM.render(
    <div>
        <CurrencyConverter codesUri="/currency/codes" converterUri="/currency/exchange" />
        

        <CurrencyHistoryApiTester uri="/currency/history" />
    </div>
    ,
    document.getElementById("root")
);

//<CurrencyHistoryPresenter uri="/currency/history" />
//<Hello text="hello" />
//<CurrencyPicker uri="/currency/codes" />
//<CurrencyPicker codes={['foo', 'bar']} onChanged={s => alert(s)} defaultSelected='bar' />
//<CurrencyConverterApiTester uri="/currency/exchange" />
//<RawApiTester uri="/currency/codes" />
//<RawApiTester uri="/currency/exchange?sourceAmount=1.2&sourceCurrencyCode=HUF&destCurrencyCode=EUR" />

export class Main {
    
}
