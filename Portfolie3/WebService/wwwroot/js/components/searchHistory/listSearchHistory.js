define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-searchHistory");
        let prev = ko.observable();
        let next = ko.observable();

        let searchHistory = ko.observableArray([]);

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
            ds.clearSearch();
            searchHistory([]);
        }


        let enableNext = ko.observable(() => next() !== undefined);




        let findSearchHistory = () => {
            ds.getSearchHistory()
                .then(data => {
                    console.log(data);
                      prev(data.prev),
                       next(data.next),
                    searchHistory(data);
                })
                .catch(error => console.log(error));
            currentView("list");
        }
        


        
        showWhenLoggedIn();
        hideWhenLoggedIn();
        findSearchHistory();

        return {
            enableNext,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,            
            currentComponent,
            currentView,
            searchHistory,
            clearSearchHistory
        }
    };
});
