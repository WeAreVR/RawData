const { data } = require("jquery");

define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observable([10]);
        let currentView = ko.observable("list-titles");
        let prev = ko.observable();
        let next = ko.observable();

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
        
        let getData = url => {
            dataService.getTitleBasics(url, data => {
                prev(data.prev || undefined);
                next(data.next || undefined);
                titleBasics(data.items)
            })
        }

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(dataService.getTitleBasicsWithPageSize(size));
        });

        getData();

        return {
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
