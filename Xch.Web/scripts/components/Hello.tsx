
import * as React from "react";
import * as ReactDOM from "react-dom";

export interface IHelloProps {
    text: string
}

export class Hello extends React.Component<IHelloProps, {}> {
    render() : React.ReactNode {
        return <div>Hello {this.props.text}!</div>;
    }

    static renderToElement(elementId: string, text : string = "From React"): void {
        ReactDOM.render(
            <Hello text={text}/>,
            document.getElementById(elementId)
        );
    }
}

