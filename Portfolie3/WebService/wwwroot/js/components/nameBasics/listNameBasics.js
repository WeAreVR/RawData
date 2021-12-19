
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
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

        
       


        let changeSingleNameView = (id) => {
            postman.publish("changeView", "single-names");
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
            currentComponent,
            currentView,
            nameBasics,
            searchNameBasics,
            selectId
        }
    };
});
