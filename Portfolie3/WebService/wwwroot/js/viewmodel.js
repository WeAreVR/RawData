
define(["knockout", "postman"], function (ko, postman) {

    let currentView = ko.observable("list-titles");

    postman.subscribe("changeView", function (data) {
        currentView(data);
    });

    return {
        currentView
    }
});