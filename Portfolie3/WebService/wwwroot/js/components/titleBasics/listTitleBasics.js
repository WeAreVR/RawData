
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");
        let prev = ko.observable();
        let next = ko.observable();

        let titleBasics = ko.observableArray([]);
        let selectId = ko.observable();
       
        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () =>
        {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                titleBasics(data);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
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
        

        let singleTitlePage = (id) => {
            console.log(id);
            postman.publish("getInfo", id);

        }

        let changeSingleView = (id) => {
            postman.publish("changeView", "single-title");
            //singleTitlePage(id);
            postman.publish("getInfo", id);

        }

               

        let toggleBookmark = (id) => {
            ds.toggleBookmark(id);
        }
        
        

        return {
            enableNext,
            toggleBookmark,
            changeSingleView,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            currentComponent,
            currentView,
            titleBasics,
            searchTitleBasics,
            selectId,
            commentSection,
            singleTitlePage
        }
    };
});
