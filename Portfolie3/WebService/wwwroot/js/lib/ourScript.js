
function toggleComments() {
    var x = document.getElementById("commentSection");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function showWhenLoggedIn() {
    var x = document.getElementById("testId");
    if (localStorage.getItem("username") != null) {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}