
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");

        let titleBasics = ko.observableArray([]);
        let selectId= ko.observable();

        ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            titleBasics(data);
        });

        let searchTitleBasics = () => {
            console.log("searchTitleBasics");
            ds.getTitleBasics(selectId(), data => {
                console.log(data);
                titleBasics(data);
            });
            currentView("list");
            selectId("");
        }

        let commentSection = () => postman.publish("changeView", "list-comments");



        return {
            currentComponent,
            currentView,
            titleBasics,
            searchTitleBasics,
            selectId,
            commentSection
        }
    };
});
