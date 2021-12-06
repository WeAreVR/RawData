
define(["knockout", "postman"], function (ko, postman) {

    //let currentView = ko.observable("list-titles");

    let menuItems = [
        { title: "Search For Titles", component: "list-titles" },
        { title: "Bookmarks", component: "list-bookmarks" }
    ];

    let currentView = ko.observable(menuItems[0].component);

    let changeContent = menuItem => {
        console.log(menuItem);
        currentView(menuItem.component)
    };

    let isActive = menuItem => {
        return menuItem.component === currentView() ? "active" : "";
    }

    postman.subscribe("changeView", function (data) {
        currentView(data);
    });

    return {
        menuItems,
        changeContent,
        isActive,
        currentView
    }
});