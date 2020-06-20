import signalR from '@microsoft/signalr';

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/GameHub")
    .build();

//let userName = '';
// получение сообщения от сервера
hubConnection.on('JoinGame', function () {

    // создаем элемент <b> для имени пользователя
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(userName + ': '));

    // создает элемент <p> для сообщения пользователя
    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);

});

// установка имени пользователя
function login(e) {
    //userName.value;
    document.getElementById("header").innerHTML = '<h3>Welcome ' + userName.value + '</h3>';
    hubConnection.invoke("Login", userName.value);
};
// отправка сообщения на сервер
function send(e) {
    hubConnection.invoke("Send", message.value);
};

hubConnection.start();