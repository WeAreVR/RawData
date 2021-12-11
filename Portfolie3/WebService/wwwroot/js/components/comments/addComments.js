define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let context = ko.observable();

        let cancel = () => {
            postman.publish("changeView", "list-comments");
        }

        let add = () => {
            postman.publish("newComment", { context: context() });
            postman.publish("changeView", "list-comments");
        }

        return {
            context,
            add,
            cancel
        }
    };
});