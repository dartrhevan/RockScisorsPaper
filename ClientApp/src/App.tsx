import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';

import './custom.css'
import Game from "./components/Game";

export default class App extends React.Component {

    render(): Object | string | number | {} | Object | Object | boolean {
        return (
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home} />
                    <Route exact path='/game' component={Game} />
                </Switch>
            </Layout>
        );
    }
}


