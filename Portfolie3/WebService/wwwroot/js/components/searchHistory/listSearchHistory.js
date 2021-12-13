
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("list-searchHistory");
        let prev = ko.observable();
        let next = ko.observable();

        let searchHistory = ko.observableArray([]);
        let selectId = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () => {
            console.log(next());
            ds.getSearchHistoryUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    searchHistory(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getSearchHistoryUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    searchHistory(data);
            });
        }

        let clearSearchHistory = () => {
            console.log('ClearSearchHistory');
            ds.clearSearch('tobias');
            searchHistory([]);
        }


        let enableNext = ko.observable(() => next() !== undefined);

        let findSearchHistory = () => {
            console.log("findSearchHistory");

            ds.getSearchHistory()
                .then(data => {
                    console.log(data);
                    prev(data.prev),
                        next(data.next),
                        searchHistory(data);})
                .catch(error => console.log(error));

            currentView("list");
            selectId("");
        }


        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getSearchHistory(ds.getSearchHistoryWithPageSize(size));
        });

       


        findSearchHistory();

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
            searchHistory,
            findSearchHistory,
            clearSearchHistory
        }
    };
});
