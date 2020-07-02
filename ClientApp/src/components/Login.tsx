import React from "react";
import send from '../Send';
import { Input, Button } from "reactstrap";

export default class Login extends React.Component {
    sendLogin = send.bind(this, '/Account/Login');

    render(): Object | string | number | {} | boolean {
        return (<div className='form'>
                    <h2>Authentication</h2>
                    Login:
                    <br />
                    <Input id='login' />
                    <br />
                    Password:
                    <br />
                    <Input id='password' type='password' />
                    <br/>
                    <Button onClick={this.sendLogin}>Submit</Button>
                </div>);
    }
}