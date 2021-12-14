
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("list-names");
        let prev = ko.observable();
        let next = ko.observable();

        let nameBasics = ko.observableArray([]);
        let selectId = ko.observable();
      
        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () =>
        {
            console.log(next());
            ds.getNameBasicsUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasics(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getNameBasicsUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasics(data);
            });
        }

        let enableNext = ko.observable(() => next() !== undefined);

        let searchNameBasics = () => {
            console.log("searchNameBasics");
            ds.getNameBasics(selectId(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasics(data);
            });
            currentView("list");
            selectId("");
        }

        
        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            searchNameBasics(ds.getNameBasicsWithPageSize(size));
        });
        
        let temp = () => {
            postman.publish("changeView", "single-names");
            postman.publish("getInfoForSingleName", "nm0000001 ");
            console.log("det burde virke");

        }

        return {
            enableNext,
            temp,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            selectedPageSize,
            pageSize,
            currentComponent,
            currentView,
            nameBasics,
            searchNameBasics,
            selectId
        }
    };
});
