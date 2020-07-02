import * as React from 'react';
import { Button } from 'reactstrap';

class Home extends React.Component {
    render(): Object | string | number | {} | boolean {
        return (
            <div>
                <h1>Rock scissor paper</h1>
                <h3>
                    This is a simple game: rock scissor paper.
                    <br />
                    You can play with or with random competitor.
                </h3>
            </div>
        );
    }

}

export default Home;
