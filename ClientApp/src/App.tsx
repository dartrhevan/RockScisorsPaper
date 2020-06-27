import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';

import './custom.css'
import Game from "./components/Game";
import Login from "./components/Login";
import Register from "./components/Register";
import Play from "./components/Play";

export default class App extends React.Component {

    render(): Object | string | number | {} | boolean {
        return (
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home} />
                    <Route exact path='/game' component={Game} />
                    <Route exact path='/game/:type' component={Play} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/register' component={Register} />
                </Switch>
            </Layout>
        );
    }
}


