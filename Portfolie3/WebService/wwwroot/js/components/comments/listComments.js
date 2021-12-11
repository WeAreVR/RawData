
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-comments");

        let comments = ko.observableArray([]);
        let selectId = ko.observable();

        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        /*ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            comments(data);
        });*/

        let showComments = () => {
            console.log("showComments");
            ds.getComments(selectId(), data => {
                console.log(data);
                comments(data);
            });
            currentView("list");
            selectId("");
        }
        let showNext = () => {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    episodes(data.items);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    episodes(data.items);
            });
        }
        let addComment = () => postman.publish("changeView", "addComments");
        


        showComments();
        return {
            currentComponent,
            addComment,
            enableNext,
            showNext,
            showPrev,
            enablePrev,
            prev,
            next,
            currentView,
            comments,
            showComments,
            selectId
        }
    };
});
