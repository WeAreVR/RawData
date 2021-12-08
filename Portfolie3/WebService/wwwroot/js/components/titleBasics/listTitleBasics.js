//const { data } = require("jquery");

//const { get } = require("jquery");

define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observable([10]);
        let currentView = ko.observable("list-titles");
        let prev = ko.observable();
        let next = ko.observable();

        let titleBasics = ko.observableArray([]);
        let selectId = ko.observable();

        ds.getTitleEpisodes(selectId, data => {
            console.log(data);
            titleBasics(data);
        });

        let getData = url => {
            ds.getTitleBasics(url, data => {
                prev(data.prev || undefined);
                next(data.next || undefined);
                titleBasics(data.items)
            })
        }
        let showPrev = titleBasics => {
            console.log(prev());
            getData(prev());
        }
        let enablePrev = ko.observable(() => prev() !== undefined);
        let showNext = titleBasics => {
            console.log(next());
            getData(next());
        }
        let enableNext = ko.observable(() => next() !== undefined);

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

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getTitleBasicsWithPageSize(size));
        });

        getData();

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
