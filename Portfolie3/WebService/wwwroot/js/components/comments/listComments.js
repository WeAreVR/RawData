
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-comments");

        let comments = ko.observableArray([]);
        let selectId = ko.observable();
        let titleId = ko.observable();
        let username = ko.observable();
        let timeStamp = ko.observable();

        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);


       
        let showComments = (id) =>
        {
            console.log("showComments");
            ds.getComments(id, data => {
                console.log(data);
                titleId(id);
                console.log(data);
                comments(data);
                prev(data.prev);
                next(data.next);
                console.log(data.items);


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
                prev(data.prev),
                    next(data.next),
                    comments(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),    
                comments(data);
            });
        }
        let addComment = () => postman.publish("changeView", "addComments");

      

        let addCommentPage = (id) => {
            console.log(id);
            postman.publish("getTitleForAddComment", id);

        }

        let changetoCommentAddView = (id) => {
            console.log("virker den ehr asd?")
            postman.publish("changeView", "addComments");
            addCommentPage(id);
        }

        let updateCommentPage = (id,content) => {
            postman.publish("getTitleAndContentForUpdateComment", {id, content });
            console.log(id);
            console.log(content);

        }

        let changetoCommentUpdateView = (id, content) => {
            console.log(id);
            console.log(content);
            postman.publish("changeView", "updateComments");
            updateCommentPage(id, content);
        }

        let del = comment => {
           // comments.remove(comment);
            ds.deleteComment(comment);
        }

        //showWhenUsernameSame();
        return {
            del,
            currentComponent,
            username,
            timeStamp,
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
