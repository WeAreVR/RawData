define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-bookmarks");

        let titleBasic = ko.observableArray([]);
        let titlePrincipals = ko.observableArray([]);
        let titleId = ko.observable();
        let selectId = ko.observable();
        let crewName = ko.observableArray([]);



        let prev = ko.observable();
        let next = ko.observable();

        let enablePrev = ko.observable(() => prev() !== undefined);
        let enableNext = ko.observable(() => next() !== undefined);

        let getCrewInfo = (id) => {
            console.log("getInfo");
            ds.getTitleBasic(id, data => {
                console.log(data);
                titleBasic(data);
                titlePrincipals(data.listTitlePrincipals);
                titleId(data.id);

            });
            ds.getTitleBasic(localStorage.getItem("username"), id, data => {
                console.log(data);
                console.log(data.setRating);

                setRating(data.rating);

            });
            currentView("list");
            selectId("");
        }

        let showNext = () => {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    bookmarks(data.items);
            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                    next(data.next),
                    bookmarks(data.items);
            });
        }

        
        console.log(titleId());

        postman.subscribe("getCrewInfo", id => {
            console.log("postmanSubscribe")
            getCrewInfo(id);
        }, "list-titles");

        let commentPage = (id) => {
            console.log(id);
            postman.publish("showComment", id);
            console.log("abe");

        }

        let goBack = () => {
            postman.publish("changeView", "single-title");

            postman.publish("getInfo", titleId);

        }

        return {
            enablePrev,
            goBack,
            crewName,
            titleBasic,
            enableNext,
            showPrev,
            showNext,
            currentComponent,
            titlePrincipals,
            currentView
        }
    };
});
