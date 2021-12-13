
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-comments");

        let comments = ko.observableArray([]);
        let selectId = ko.observable();
        let titleId = ko.observable();


        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        /*ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            comments(data);
        });*/

       
        let showComments = (id) =>
        {
            console.log("showComments");
            ds.getComments(id, data => {
                console.log(data);
                titleId(id);
                comments(data);
            });
            currentView("list");
            selectId("");
        }
            postman.subscribe("showComment", id => {
                console.log(id)
                showComments(id);
            }, "list-comments");

        
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

      

        let addCommentPage = (id) => {
            console.log(id);
            postman.publish("getTitleForAddComment", id);
            console.log("abe");

        }

        let changetoCommentAddView = (id) => {
            postman.publish("changeView", "addComments");
            addCommentPage(id);
        }

        let updateCommentPage = (id,content) => {
            postman.publish("getTitleAndContentForUpdateComment", (id,content));
            console.log("abe");

        }

        let changetoCommentUpdateView = (id, content) => {
            postman.publish("changeView", "updateComments");
            updateCommentPage(id,content);
        }
        return {
            currentComponent,
            changetoCommentUpdateView,
            updateCommentPage,
            changetoCommentAddView,
            titleId,
            addCommentPage,
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
