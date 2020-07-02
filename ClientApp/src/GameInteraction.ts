import* as signalR from '@microsoft/signalr';

const options: signalR.IHttpConnectionOptions =
{
    accessTokenFactory: () =>
        sessionStorage.getItem('token') as string
};


export enum GameValue {
    Rock = 'Rock',
    Scissors = 'Scissors',
    Paper = 'Paper'
}

export class GameHubClient {

    private hubConnection: signalR.HubConnection;

    constructor(onStartGame : Function) {

        const hub = new signalR.HubConnectionBuilder()
            .withUrl("/GameHub", options)
            .build();
        this.hubConnection = hub;
        // получение сообщения от сервера
        this.hubConnection.on('startGame', competitor => onStartGame(competitor));
        
        this.hubConnection.on("leaveGame",
            msg => {
                alert('Your competitor left game');
                this.hubConnection.stop();
                window.location.href = '/game';
            });

        this.hubConnection.on("playResult",
            msg => alert(msg));

    }
    async play(value: GameValue) {
        this.hubConnection.invoke("Play", value.toString());
    }

    async leaveGame() {
        //this.hubConnection.invoke("LeaveGame");
        await this.hubConnection.stop();
    }

    async joinGame(gameType: string = "RandomCompetitor") {
        if (this.hubConnection.state !== signalR.HubConnectionState.Connected)
            await this.hubConnection.start();
        this.hubConnection.invoke("JoinGame", gameType);
    };
}
