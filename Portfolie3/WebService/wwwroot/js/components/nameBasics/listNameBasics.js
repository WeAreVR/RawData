
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
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasics(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
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
        
        

        let singleNamePage = (id) => {
            console.log(id);
            postman.publish("getInfo", id);
            console.log("abe");

        }

        let changeSingleNameView = (id) => {
            postman.publish("changeView", "single-names");
           // singleTitlePage(id);
            postman.publish("getInfoForSingleName", id);

        }

        return {
            enableNext,
            changeSingleNameView,
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
