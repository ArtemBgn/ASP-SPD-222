document.addEventListener('DOMContentLoaded', () => {//auth-password
    const authButton = document.getElementById("auth-button")
    if (authButton) authButton.addEventListener('click', authButtonClick);

    const saveProfileButton = document.getElementById("profile-save-button")
    if (saveProfileButton) saveProfileButton.addEventListener('click', saveProfileButtonClick);

    const exitProfileButton = document.getElementById("profile-exit-button")
    if (exitProfileButton) exitProfileButton.addEventListener('click', exitProfileButtonClick);

});

function authButtonClick() {
    const loginInput = document.getElementById("auth-login");
    if (!loginInput) throw "Element #auth-login not found";
    const login = loginInput.value.trim();
    if (login.length == 0) {
        showAuthMessage("Логін не може бути порожнім");
        return;
    }
    const passwordInput = document.getElementById("auth-password");
    if (!passwordInput) throw "Element #auth-password not found";
    const password = passwordInput.value.trim();
    if (password.length == 0) {
        showAuthMessage("Пароль не може бути порожнім");
        return;
    }
    fetch(`/api/auth?login=${login}&password=${password}`)
        .then(r => {
            if (r.status == 200) { //ok
                window.location.reload();
            }
            else { //401
                showAuthMessage("Вхід відхилено");
            }
        }
        /*
            r.json())
        .then(j => {
            console.log(j);
            showAuthMessage(j.status);
        }*/);
}
function showAuthMessage(message) {
    const authMessage = document.getElementById("auth-message");
    if (!authMessage) throw "Element #auth-message not found";
    authMessage.innerText = message;
    authMessage.classList.remove("visually-hidden");
}
/*auth-signout-button*/
//profile-save-button
function saveProfileButtonClick() {
    const nameInput = document.querySelector('input[name="profile-name"]');
    if (!nameInput) throw 'Element input[name="profile-name"] not found';
    const emailInput = document.querySelector('input[name="profile-email"]');
    if (!emailInput) throw 'Element input[name="profile-email"] not found';
    fetch(`/user/UpdateProfile?newName=${nameInput.value}&newEmail=${emailInput.value}`, { method: 'POST' })
        .then(r => r.json())
        .then(j => {
            console.log(j);
            window.location.reload();
        });
}
function exitProfileButtonClick() {
    
    fetch(`/api/auth`, { method: 'DELETE' })
        .then(r => {
            console.log("Кнопка працює");
            window.location.href = "/Home/Index";
        });
}