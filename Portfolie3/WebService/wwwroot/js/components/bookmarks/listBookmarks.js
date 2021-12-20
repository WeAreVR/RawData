define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-bookmarks");
        let bookmarks = ko.observableArray([]);

        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

     
       
        let findBookmark = () => {
            ds.getBookmarks()
                .then(data => {
                    console.log(data);
                    prev(data.prev),
                        next(data.next),
                        bookmarks(data.items);
                })
                .catch(error => console.log(error));
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
        let toggleBookmark = (id) => {

            ds.toggleBookmark(id);
            postman.publish("changeView", "list-bookmarks");

        }

      

        findBookmark();
        showWhenLoggedIn();
        hideWhenLoggedIn();
        

        return {
            enablePrev,
            enableNext,
            showPrev,
            showNext,
            currentComponent,
            currentView,
            bookmarks,
            toggleBookmark,
            findBookmark,
            checkLogin
        }
    };
});
