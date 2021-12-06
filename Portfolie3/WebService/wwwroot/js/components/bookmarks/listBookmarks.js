define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");
        let bookmarks = ko.observableArray([]);

        let getBookmarks = () => {
            console.log("getBookmarks");
            ds.getBookmarks(data => {
                console.log(data);
                ratings(data);
            });
            currentView("list");
        }


        return {
            currentComponent,
            currentView,
            bookmarks,
            getBookmarks
        }
    };
});
