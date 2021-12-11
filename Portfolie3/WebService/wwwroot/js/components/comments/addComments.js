define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let context = ko.observable();
        let currentView = ko.observable("addComment");

        let cancel = () => {
            postman.publish("changeView", "list-comments");
        }

        let add = () => {
            postman.publish("newComment", { username: "testuser", titleId = "tt0312280", content: context()});
            postman.publish("changeView", "list-comments");
        }

        postman.subscribe("newComment", comment => {
            ds.ddddd(user, newUser => {
                console.log("postmanSubscribe")
            });
        }, "list-comments");

        return {
            context,
            currentView,
            add,
            cancel
        }
    };
});