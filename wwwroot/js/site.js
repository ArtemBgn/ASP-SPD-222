document.addEventListener('DOMContentLoaded', () => {//auth-password
    const authButton = document.getElementById("auth-button")
    if (authButton) authButton.addEventListener('click', authButtonClick);

    const saveProfileButton = document.getElementById("profile-save-button")
    if (saveProfileButton) saveProfileButton.addEventListener('click', saveProfileButtonClick);

    const exitProfileButton = document.getElementById("profile-exit-button")
    if (exitProfileButton) exitProfileButton.addEventListener('click', exitProfileButtonClick);

    const offProfileButton = document.getElementById("profile-off-button")
    if (offProfileButton) offProfileButton.addEventListener('click', offProfileButtonClick);

    const deleteProfileButton = document.getElementById("profile-delete-button")
    if (deleteProfileButton) deleteProfileButton.addEventListener('click', deleteProfileButtonClick);

    const recoveryProfileButton = document.getElementById("profile-recovery-button")
    if (recoveryProfileButton) recoveryProfileButton.addEventListener('click', recoveryProfileButtonClick);
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
            if (r.ok) {
                console.log("Кнопка працює");
                //showAuthMessage2(r.status);
                window.location.href = "/Home/Index";
            }
            else {
                showAuthMessage2(r.status +" - Server error");
            }
        });
}
function showAuthMessage2(message) {
    const authMessage2 = document.getElementById("auth-message2");
    if (!authMessage2) throw "Element #auth-message not found";
    authMessage2.innerText = message;
    authMessage2.classList.remove("visually-hidden");
}
function offProfileButtonClick() {
    fetch(`/user/OffProfile`, { method: 'GET' })
        .then(r => r.json())
        .then(j => {
            console.log(j);
            exitProfileButtonClick();
        });
}
function deleteProfileButtonClick() {
    fetch(`/user/DeleteProfile`, { method: 'DELETE' })
        .then(r => r.json())
        .then(j => {
            console.log(j);
            exitProfileButtonClick();
        });
}
//function showAuthMessage3(message) {
//    const authMessage3 = document.getElementById("auth-message3");
//    if (!authMessage3) throw "Element #auth-message not found";
//    authMessage3.innerText = message;
//    authMessage3.classList.remove("visually-hidden");
//}

function recoveryProfileButtonClick() {
    fetch(`/user/RecoveryProfile`, { method: 'GET' })
        .then(r => r.json())
        .then(j => {
            console.log(j);
            window.location.reload();
        });
}