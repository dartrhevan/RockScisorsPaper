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

function login() {
    //userName.value;
    //document.getElementById("header").innerHTML = '<h3>Welcome ' + userName.value + '</h3>';
    //hubConnection.invoke("Login", userName.value);
};

export async function send() {
    if(hubConnection.state !== signalR.HubConnectionState.Connected)
        await hubConnection.start();
    hubConnection.invoke("Greetings");
};

