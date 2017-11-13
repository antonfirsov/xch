import * as React from "react";
import * as ReactDOM from "react-dom";

import { Ajax } from "../utils/Ajax";

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

interface IRawApiTesterProps {
    uri : string;
}

export class RawApiTester extends React.Component<IRawApiTesterProps, IApiTesterState> {
    
    render(): React.ReactNode {

        const TheTester = ApiTester as new () => ApiTester<string>;

        var callFun = () => Ajax.get<string>(this.props.uri);

        return (
            <TheTester name="karesz" callApi={callFun}>
            </TheTester>
        );
    }
}