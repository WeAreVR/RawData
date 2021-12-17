
function toggleComments() {
    var x = document.getElementById("commentSection");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function loggedIn() {
    if (localStorage.getItem("username") !== null) {
        return true;
    }
    return false;
}

function showWhenLoggedIn() {
    var x = document.getElementById("loginButton");
    if (localStorage.getItem("username") != null) {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    } 
}
