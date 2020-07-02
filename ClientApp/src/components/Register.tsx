import React from "react";

import { Input, Button } from "reactstrap";
import send from "../Send";


export default class Register extends React.Component {

    sendReg = send.bind(this, '/Account/Register');
    render(): Object | string | number | {} | boolean {
        return (<div className='form'>
                    <h2>Registration</h2>
                    Login:
                    <br />
                    <Input id='login' />
                    <br />
                    Password:
                    <br />
                    <Input id='password' type='password'/>
                    <br/>
                    <Button onClick={this.sendReg}>Submit</Button>
                </div>);
    }
}