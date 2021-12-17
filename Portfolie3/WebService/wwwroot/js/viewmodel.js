define(["knockout", "postman"], function (ko, postman) {

    /*let menuItems = [
        { title: "Find titles", component: "list-titles" },
        { title: "Find people", component: "list-names" },
        { title: "Bookmark", component: "list-bookmarks" },
        { title: "Search history", component: "list-searchHistory" },
        { title: "TEST", component: "single-title" }
    ];*/

    let menuItems = ko.observable();

    let setMenuItems = () => {
       // localStorage.removeItem("username");
        if (localStorage.getItem("username") !== null) {
            menuItems = [
                { title: "Find titles", component: "list-titles" },
                { title: "Find people", component: "list-names" },
                { title: "Bookmark", component: "list-bookmarks" },
                { title: "Search history", component: "list-searchHistory" },
                { title: "TEST", component: "single-title" }
            ];
            return (menuItems);
        }
        else if (localStorage.getItem("username") === null) {
            menuItems = [
                { title: "Find titles", component: "list-titles" },
                { title: "Find people", component: "list-names" },
                { title: "TEST", component: "single-title" }
            ];
            return (menuItems)
        };
    }

    setMenuItems();


    let currentView = ko.observable(menuItems[0].component);

    let changeContent = menuItem => {
        setMenuItems();
        console.log(menuItem);
        currentView(menuItem.component)
    };

    let loginPage = () => {
        console.log("loginPage");
        currentView("login")
    };

    let isActive = menuItem => {
        return menuItem.component === currentView() ? "active" : "";
    }

    postman.subscribe("changeView", function (data) {
        currentView(data);
    });



    return {
        currentView,
        menuItems,
        changeContent,
        isActive,
        loginPage,
        setMenuItems
    }
});