define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let context = ko.observable();
        let test = ko.observable("hej");

        let titleId = ko.observable();
        let currentView = ko.observable("addComment");

        let cancel = () => {
            console.log("id er");
            console.log(titleId());

            changetoCommentView(titleId());

        }

        
        postman.subscribe("getTitleAndContentForUpdateComment", content => {
            console.log("postmanSubscribe")
            console.log(content.content);
            context(content.content);
            titleId(content.id);
            console.log("vi er i title");
            console.log(content.id);
        }, "list-titles");

      

       
        postman.subscribe("updateComment", comment => {
            ds.updateComment(comment, updateComment => {
                console.log("postmanSubscribe")
            });
        }, "list-comments");


        let update = () => {
            console.log(context())
            postman.publish("updateComment", { username: localStorage.getItem("username"), titleId: titleId(), content: context() });
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