
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("list-titles");
        let prev = ko.observable();
        let next = ko.observable();

        let titleBasics = ko.observableArray([]);
        let selectId = ko.observable();

        ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            titleBasics(data);
        });

       
        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () =>
        {
            console.log(next());
            ds.getTitleBasicsUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    titleBasics(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getTitleBasicsUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    titleBasics(data);
            });
        }

        let enableNext = ko.observable(() => next() !== undefined);

        let searchTitleBasics = () => {
            console.log("searchTitleBasics");
            ds.getTitleBasics(selectId(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                titleBasics(data);
            });
            currentView("list");
            selectId("");
        }

        let commentSection = () => postman.publish("changeView", "list-comments");
        
        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            searchTitleBasics(ds.getTitleBasicsWithPageSize(size));
        });
        

        return {
            enableNext,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            selectedPageSize,
            pageSize,
            currentComponent,
            currentView,
            titleBasics,
            searchTitleBasics,
            selectId,
            commentSection
        }
    };
});
