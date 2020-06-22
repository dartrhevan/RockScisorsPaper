import* as signalR from '@microsoft/signalr';

const options: signalR.IHttpConnectionOptions =
{
    accessTokenFactory: () =>
        sessionStorage.getItem('token') as string
};

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/GameHub", options)
    .build();

//let userName = '';
// получение сообщения от сервера
hubConnection.on('start', function (competitor : string) {

});

hubConnection.on("Greetings", mes => {
    alert(mes);
});

export async function send() {
    if(hubConnection.state !== signalR.HubConnectionState.Connected)
        await hubConnection.start();
    hubConnection.invoke("JoinGame", "RandomCompetitor", "");
};

