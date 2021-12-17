
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

var menuItemsLoggedIn = [
    { title: "Find titles", component: "list-titles" },
    { title: "Find people", component: "list-names" },
    { title: "Bookmark", component: "list-bookmarks" },
    { title: "Search history", component: "list-searchHistory" },
    { title: "TEST", component: "single-title" }
];

var menuItem = [
    { title: "Find titles", component: "list-titles" },
    { title: "Find people", component: "list-names" },
    { title: "TEST", component: "single-title" }
];

let menuItems123 = () => {
    localStorage.removeItem("username");
    if (localStorage.getItem("username") != null) {
        return (menuItemsLoggedIn);
    }
    else if (localStorage.getItem("username") = null) {
        return (menuItem)
    };
}
