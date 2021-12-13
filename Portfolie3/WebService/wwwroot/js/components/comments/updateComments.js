define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let context = ko.observable();
        let test = ko.observable("hej");

        let titleId = ko.observable();
        let currentView = ko.observable("addComment");

        let cancel = () => {
            postman.publish("changeView", "list-comments");
        }
       
        postman.subscribe("getTitleAndContentForUpdateComment", (id,content) => {
            console.log("postmanSubscribe")
            titleId = id;
            console.log("vi er her");
            console.log(id);
            console.log(content);
            context(content);

        }, "list-titles");

        // titel over bruger vi ikke?
        postman.subscribe("updateComment", comment => {
            ds.updateComment(comment, updateComment => {
                console.log("postmanSubscribe")
            });
        }, "list-comments");


        let update = () => {
            console.log(context())
            postman.publish("updateComment", { username: "testuser", titleId: titleId(), content: context() });
            changetoCommentView(titleId());
        }

        let commentPage = (id) => {
            console.log(id);
            postman.publish("showComment", id);
            console.log("abe");

        }

        let changetoCommentView = (id) => {
            postman.publish("changeView", "list-comments");
            commentPage(id);
        }


        return {
            context,
            test,
            commentPage,
            changetoCommentView,
            titleId,
            currentView,
            update,
            cancel
        }
    };
});