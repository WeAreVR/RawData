
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-episodes");

        let episodes = ko.observableArray([]);
        let selectId = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let titleId = ko.observable()

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        let searchTitleEpisodes = (id ) => {
            console.log("searchTitleEpisodes");

            ds.getTitleEpisodes(id, data => {
                console.log(data);
                episodes(data.items);
                prev(data.prev);
                next(data.next);
                titleId(id);

            });
            currentView("list");
            selectId("");
        }

        postman.subscribe("getsearchTitleEpisodes", id => {
            console.log("postmanSubscribe")
            searchTitleEpisodes(id);
        }, "list-titles");


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
        let goBack = () => {
            postman.publish("changeView", "single-title");
            postman.publish("getInfo", titleId());

        }

        return {
            enablePrev,
            goBack,
            enableNext,
            showPrev,
            showNext,
            currentComponent,
            currentView,
            episodes,
            titleId,
            searchTitleEpisodes,
            selectId
        }
    };
});
