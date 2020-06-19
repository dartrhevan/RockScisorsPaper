export default (url : string) => {
    const body = new FormData();
    body.append('login', (document.getElementById('login') as HTMLInputElement).value);
    body.append('password', (document.getElementById('password') as HTMLInputElement).value);
    fetch(url,
            {
                method: 'POST',
                body: body
            }).then(r => r.json())
        .then(r => {
            if (r.message) {
                alert(r.message);
                return;
            }
            alert('Hello ' + r.username);
            sessionStorage.setItem('token', r.access_token);
            sessionStorage.setItem('username', r.username);
            window.location.href = '/';
        });
};