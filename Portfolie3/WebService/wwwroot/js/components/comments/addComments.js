define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let context = ko.observable();
        let titleId = ko.observable();
        let currentView = ko.observable("addComment");

        let cancel = () => {
            changetoCommentView(titleId());
        }
       
       

        postman.subscribe("getTitleForAddComment", id => {
            console.log("postmanSubscribe")
            titleId = id;
            console.log("vi er her");
            console.log(id);

        }, "list-titles");


        postman.subscribe("newComment", comment => {
            ds.addComment(comment, newComment => {
                console.log("postmanSubscribe")
            });
        }, "list-comments");


        let add = () => {
            console.log(context())
            postman.publish("newComment", { username: "testuser", titleId: titleId(), content: context() });
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
            commentPage,
            changetoCommentView,
            titleId,
            currentView,
            add,
            cancel
        }
    };
});