import React from "react";
import { Button, Input } from "reactstrap";
import $ from "jquery";

export default class Game extends React.Component {

    render(): Object | string | number | {} | boolean {
        return (
            <div className="centerd">
                <h2>Play</h2>
                <br />
                <Button onClick={() => window.location.href = '/game/RandomCompetitor'}>With random competitor</Button>
                <br />
                <br />
                <Button onClick={() => window.location.href = '/game/Bot'}>With bot</Button>
                <br />
            </div>);
    }
}