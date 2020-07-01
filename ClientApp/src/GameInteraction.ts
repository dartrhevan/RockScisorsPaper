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

        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/GameHub", options)
            .build();

        // получение сообщения от сервера
        this.hubConnection.on('startGame', competitor => {
            alert('Your competitor is:' + competitor);
            onStartGame(competitor);
        });

        this.hubConnection.on("playResult",
            msg => {
                alert(msg);
            });

        this.hubConnection.onclose(this.onClose);
    }

    private onClose() {
        this.hubConnection.invoke('LeaveGame'); //TODO: Check
        alert('close');
    }

    async play(value: GameValue) {
        this.hubConnection.invoke("Play", value.toString());
    }

    async joinGame(gameType: string = "RandomCompetitor") {
        if (this.hubConnection.state !== signalR.HubConnectionState.Connected)
            await this.hubConnection.start();
        this.hubConnection.invoke("JoinGame", gameType);
        alert('Wait for a competitor...');
    };
}
