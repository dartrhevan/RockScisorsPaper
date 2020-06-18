import 'bootstrap/dist/css/bootstrap.css';
import {Router} from 'react-router'
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { createBrowserHistory } from 'history';
import App from './App';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;

const history = createBrowserHistory();

ReactDOM.render(
        (<Router history={history}>
            <App />,
        </Router>),
    document.getElementById('root'));

