import * as React from "react";
import * as ReactDOM from "react-dom";

import { Ajax } from "../utils/Ajax";

interface IApiTesterProps {
    uri: string;
    callApi: () => string;
}

interface IApiTesterState {
    result:string;
}

// No idea how to make Jasmine work in Visual Studio with the current client stack :(
export class ApiTester extends React.Component<IApiTesterProps, IApiTesterState> {
    constructor(props: IApiTesterProps) {
        super(props);
        this.state = { result: "SENDING.." };
    }
    
    render(): React.ReactNode {
        return (
            <div>
                <p>Calling <strong>{this.props.uri}</strong></p>
                <p>Result:</p>
                <p>{this.state.result}</p>
                <hr/>
            </div>
        );
    }

    componentDidMount(): void {
        Ajax.get(this.props.uri).then(result => {
            this.setState({ result: result });
            this.forceUpdate();
        });
    }
}