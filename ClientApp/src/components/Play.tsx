import React from 'react';
import { RouteComponentProps } from 'react-router';
import { GameHubClient, GameValue } from '../GameInteraction';

interface IType {
    type : string
}

interface IParams {
    params: IType
}

interface IProps {
     match: IParams
}

export default class Play extends React.Component {
    constructor(props: IProps) {
        super(props);
        console.log(GameValue.Paper.toString());
        this.type = props.match.params.type;
        this.state = { isPlaying: false };
    }

    private type : string;

    render(): Object | string | number | {}  {
        
        const client = new GameHubClient((competitor: string) => { });
        
        client.joinGame(this.type);

        return (
            <div>
                <button>Rock</button>
                <button>Scissors</button>
                <button>Paper</button>
            </div>);
    }
}