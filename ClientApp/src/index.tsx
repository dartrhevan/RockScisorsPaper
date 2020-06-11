import 'bootstrap/dist/css/bootstrap.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { createBrowserHistory } from 'history';
import App from './App';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;

ReactDOM.render(
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    document.getElementById('root'));

