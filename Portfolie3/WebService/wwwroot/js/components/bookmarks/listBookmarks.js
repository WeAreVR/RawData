define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-bookmarks");
        let bookmarks = ko.observableArray([]);

        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        let getBookmarks = () => {
            console.log("getBookmarks");
            ds.getBookmarks(data => {
                console.log(data);
                bookmarks(data.items);
            });
            currentView("list");
        }
        let showNext = () => {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    bookmarks(data.items);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    bookmarks(data.items);
            });
        }

        let checkLogin = () => {
            if (localStorage.getItem("username") === null) {
                    return false;
                }
                return true;
        }

        showWhenLoggedIn();
        hideWhenLoggedIn();
        getBookmarks();

        return {
            enablePrev,
            enableNext,
            showPrev,
            showNext,
            currentComponent,
            currentView,
            bookmarks,
            getBookmarks,
            checkLogin
        }
    };
});
