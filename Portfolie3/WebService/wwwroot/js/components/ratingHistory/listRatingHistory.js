define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");
        let ratings = ko.observableArray([]);

        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        let getRatingHistory = () => {
            console.log("getRatingHistory");
            ds.getRatingHistory(data => {
                console.log(data);
                ratings(data);
            });
            currentView("list");
        }
        let showNext = () => {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    ratings(data.items);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    ratings(data.items);
            });
        }


        return {
            enableNext,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            currentComponent,
            currentView,
            ratings,
            getRatingHistory
        }
    };
});
