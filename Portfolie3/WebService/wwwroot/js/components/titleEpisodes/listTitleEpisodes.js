
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-episodes");

        let episodes = ko.observableArray([]);
        let selectId = ko.observable();

        ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            episodes(data);

        });

        let searchTitleEpisodes = () => {
            console.log("searchTitleEpisodes");
            ds.getTitleEpisodes(selectId(), data => {
                console.log(data);
                episodes(data);

            });
            currentView("list");
            selectId("");
        }

        return {
            currentComponent,
            currentView,
            episodes,
            searchTitleEpisodes,
            selectId
        }
    };
});
