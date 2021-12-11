﻿
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("list-titles");
        let prev = ko.observable();
        let next = ko.observable();

        let titleBasics = ko.observableArray([]);
        let selectId = ko.observable();
       
        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () =>
        {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                titleBasics(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                titleBasics(data);
            });
        }

        let enableNext = ko.observable(() => next() !== undefined);

        let searchTitleBasics = () => {
            console.log("searchTitleBasics");
            ds.getTitleBasics(selectId(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                titleBasics(data);
            });
            currentView("list");
            selectId("");
        }

        let commentSection = () => postman.publish("changeView", "list-comments");

        let singleTitlePage = () => postman.publish("changeView", "single-title");

        
       
        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            searchTitleBasics(ds.getTitleBasicsWithPageSize(size));
        });


        /*postman.subscribe("newBookmark", bookmark => {
            ds.createBookmark(bookmark, newBookmark)
        }, "list-bookmarks");


        let addBookmark = () => {
            postman.publish("newBookmark", { titleId = 'tt8451992 ' });
            // postman.publish("changeView", "list-categories");
        }*/

        let addBookmark = () => {
            ds.createBookmark('tt0993846 ');
        }
        

        return {
            enableNext,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            selectedPageSize,
            pageSize,
            currentComponent,
            currentView,
            titleBasics,
            searchTitleBasics,
            selectId,
            commentSection,
            singleTitlePage,
            addBookmark
        }
    };
});
