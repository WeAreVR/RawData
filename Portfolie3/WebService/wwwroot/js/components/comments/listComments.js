
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-comments");

        let comments = ko.observableArray([]);
        let selectId= ko.observable();

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



        return {
            currentComponent,
            currentView,
            comments,
            showComments,
            selectId
        }
    };
});
