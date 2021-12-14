
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-episodes");

        let episodes = ko.observableArray([]);
        let selectId = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();


        let toEpisodeList = () => postman.publish("changeView", "list-episodes");

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        let searchTitleEpisodes = () => {
            console.log("searchTitleEpisodes");

            ds.getTitleEpisodes(selectId(), data => {
                console.log(data);
                episodes(data.items);
                prev(data.prev);
                next(data.next);

            });
            currentView("list");
            selectId("");
        }

        let showNext = () => {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                episodes(data.items);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                episodes(data.items);
            });
        }

        return {
            enablePrev,
            enableNext,
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
