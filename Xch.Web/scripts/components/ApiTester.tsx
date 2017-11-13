import * as React from "react";
import { Ajax, } from "../utils/Ajax";
import { IUriProps, ICurrencyConverterParameters, ICurrencyHistory, flattenCurrencyHistoryData } from "../utils/Data";


interface IApiTesterProps<T> {
    name:string;
    callApi: () => Promise<T>;
}

interface IApiTesterState {
    result:string;
}

// No idea how to make Jasmine work in Visual Studio with the current client stack :(
export class ApiTester<T> extends React.Component<IApiTesterProps<T>, IApiTesterState> {
    constructor(props: IApiTesterProps<T>) {
        if (!props.name) {
            props.name = "API";
        }
        super(props);
        this.state = { result: "SENDING.." };
    }

    render(): React.ReactNode {
        return (
            <div>
                <p>Calling <strong>{this.props.name}</strong></p>
                <p>Result:</p>
                <p>{this.state.result}</p>
                <hr/>
            </div>
        );
    }

    componentDidMount(): void {
        this.props.callApi().then(result => {
            var str = JSON.stringify(result);
            this.setState({result:str});
        });
    }
}


export class RawApiTester extends React.Component<IUriProps, IApiTesterState> {
    
    render(): React.ReactNode {

        const TheTester = ApiTester as new () => ApiTester<string>;

        var callFun = () => Ajax.get<string>(this.props.uri);

        return (
            <TheTester name="karesz" callApi={callFun}>
            </TheTester>
        );
    }
}

export class CurrencyConverterApiTester extends React.Component<IUriProps, IApiTesterState> {
    render(): React.ReactNode {

        const TheTester = ApiTester as new () => ApiTester<string>;

        var parameters: ICurrencyConverterParameters = {
            sourceAmount : 400,
            sourceCurrencyCode : "HUF",
            destCurrencyCode : "EUR"
        };

        var callFun = () => Ajax.get<string>(this.props.uri, parameters);

        return (
            <TheTester name="karesz" callApi={callFun}>
            </TheTester>
        );
    }
}

export class CurrencyHistoryApiTester extends React.Component<IUriProps, IApiTesterState> {
    constructor(props: IUriProps) {
        super(props);
    }

    render(): React.ReactNode {

        const TheTester = ApiTester as new() => ApiTester<number[][]>;

        var callFun = () =>
            Ajax.get<ICurrencyHistory>(this.props.uri)
            .then(
                history => flattenCurrencyHistoryData(history, 0)
            );

        return (
            <TheTester name="karesz" callApi={callFun}>
            </TheTester>
        );
    }
}