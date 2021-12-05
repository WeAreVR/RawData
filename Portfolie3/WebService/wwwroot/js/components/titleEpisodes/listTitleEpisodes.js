
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-episodes");

        let episodes = ko.observableArray([]);
        let selectId = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();


        let toEpisodeList = () => postman.publish("changeView", "list-episodes");

        let searchTitleEpisodes = () => {
            console.log("searchTitleEpisodes");
            ds.getTitleEpisodes(selectId(), data => {
                console.log(data);
                episodes(data.items);
                prev(data.prev);


            });
            currentView("list");
            selectId("");
        }

        let showPrev = episode => {
            console.log(prev);

        }
        let showNext = episode => {
            console.log(episode);

        }

        return {
            showPrev,
            showNext,
            currentComponent,
            currentView,
            episodes,
            searchTitleEpisodes,
            selectId
        }
    };
});
