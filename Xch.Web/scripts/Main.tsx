global.Promise = require('es6-promise').Promise;

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
        <h2>Converter:</h2>
        <CurrencyConverter codesUri="/currency/codes" converterUri="/currency/exchange" />

        <h2>History:</h2>
        <CurrencyHistoryPresenter uri="/currency/history" />
    </div>
    ,
    document.getElementById("root")
);

//

//<CurrencyHistoryApiTester uri="/currency/history" />
//<CurrencyHistoryPresenter uri="/currency/history" />
//<Hello text="hello" />
//<CurrencyPicker uri="/currency/codes" />
//<CurrencyPicker codes={['foo', 'bar']} onChanged={s => alert(s)} defaultSelected='bar' />
//<CurrencyConverterApiTester uri="/currency/exchange" />
//<RawApiTester uri="/currency/codes" />
//<RawApiTester uri="/currency/exchange?sourceAmount=1.2&sourceCurrencyCode=HUF&destCurrencyCode=EUR" />

export class Main {
    
}
