import React from "react";
import { Button, Input } from "reactstrap";
import $ from "jquery";

export default class Game extends React.Component {

    show() {
        $('.competitor-choose').fadeIn(400);
    }

    hide() {
        $('.competitor-choose').fadeOut(400);
    }

    render(): Object | string | number | {} | boolean {
        return (
            <div className="centerd">
                <h2>Play</h2>
                <br />
                <Button>With random competitor</Button>
                <br />
                <br />
                <Button onClick={this.show}>Choose competitor</Button>
                <div className="competitor-choose">
                    <h5>Competitor login</h5>
                    <Input />
                    <br/>
                    <Button>Submit</Button>
                    <Button onClick={this.hide}>Cancel</Button>
                </div>
                <br />
                <br />
                <Button>With bot</Button>
                <br />
            </div>);
    }
}