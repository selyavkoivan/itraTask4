function passwordValidation() {
        let password = document.getElementsByName("password")[0]
        let repeatedPassword = document.getElementsByName("password_repeat")[0]
    if(password.value !== "" && repeatedPassword.value !== "") colouring(password, repeatedPassword)
    else password.style.borderColor = repeatedPassword.style.borderColor = document.getElementsByName("name")[0].style.borderColor
}
function repeatPasswordValidation() {
    let password = document.getElementsByName("password")[0]
    let repeatedPassword = document.getElementsByName("password_repeat")[0]
    if(password.value !== "" && repeatedPassword.value !== "") colouring(repeatedPassword, password)
    else password.style.borderColor = repeatedPassword.style.borderColor = document.getElementsByName("name")[0].style.borderColor
}

function colouring(activeArea, inactiveArea) {
    if (activeArea.value !== inactiveArea.value) {
        activeArea.style.borderColor = inactiveArea.style.borderColor = "#FF3333"
    }
    else
    {
        inactiveArea.style.borderColor = activeArea.style.borderColor = document.getElementsByName("name")[0].style.borderColor
    }
  
}
