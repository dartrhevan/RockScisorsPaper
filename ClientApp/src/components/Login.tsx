import React from "react";
import send from '../Send';
import { Input, Button } from "reactstrap";

export default class Login extends React.Component {
    sendLogin = send.bind(this, '/Account/Login');

    render(): Object | string | number | {} | Object | Object | boolean {
        return (<div>
                    <h2>Authentication</h2>
                    Login:
                    <Input id='login'/>
                    Password:
                    <Input id='password' type='password' />
                    <br/>
                    <Button onClick={this.sendLogin}>Submit</Button>
                </div>);
    }
}