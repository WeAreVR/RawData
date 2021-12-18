
function toggleComments() {
    var x = document.getElementById("commentSection");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function loggedIn() {
    if (localStorage.getItem("username") === null) {
        console.log("user not logged in");
        return false;
    }
    console.log("user logged in");
    return true;

}

function hideWhenLoggedIn() {
    var x = document.getElementById("hideWhenLoggedIn");
    if (localStorage.getItem("username") === null) {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    } 
}

function showWhenLoggedIn() {
    var x = document.getElementById("showWhenLoggedIn");
    if (localStorage.getItem("username") !== null) {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function showWhenUsernameSame(username) {
    var x = document.getElementById("showWhenUsernameSame");
    if (localStorage.getItem("username") === username) {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
