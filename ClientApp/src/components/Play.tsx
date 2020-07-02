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

interface IPlayState {
    isPlaying: boolean;
    competitor: string;
}


export default class Play extends React.Component<IProps, IPlayState> {
    constructor(props: IProps) {
        super(props);
        console.log(GameValue.Paper.toString());
        const type = props.match.params.type;
        this.state = { isPlaying: false, competitor: "" };

        this.client = new GameHubClient((competitor: string) => this.setState({ isPlaying: true, competitor }));

        this.client.joinGame(type);

    }

    componentWillUnmount(): void {
        this.client.leaveGame();
    }

    private client: GameHubClient;

    render(): Object | string | number | {}  {
        return (
            <div>
                {this.state.isPlaying ?
                    (<div className="playButtons">
                        <h2>Your competitor is: {this.state.competitor}</h2>
                        <br/>
                        <div>
                            <img width="100" draggable={false} onClick={e => this.client.play(GameValue.Rock)} height="100" src="/img/rock.png" />
                            <img width="100" draggable={false} onClick={e => this.client.play(GameValue.Paper)} height="100" src="/img/paper.png" />
                            <img width="100" draggable={false} onClick={e => this.client.play(GameValue.Scissors)} height="100" src="/img/scissors.png"/>
                        </div>
                    </div>) 
                    : <h2>Waiting for competitor</h2>}
            </div>);
    }
}