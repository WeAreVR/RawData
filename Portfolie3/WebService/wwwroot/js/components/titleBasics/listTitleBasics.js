
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");

        let titleBasics = ko.observableArray([]);
        let searchInput = ko.observable();

        ds.getTitleBasicsBySearch(searchInput, data => {
            console.log(data);
            titleBasics(data);
        });

        let searchTitleBasics = () => {
            console.log("searchTitleBasics");
            ds.getTitleBasicsBySearch(searchInput(), data => {
                console.log(data);
                titleBasics(data);
            });
            currentView("list");
            searchInput("");
        }



        return {
            currentComponent,
            currentView,
            titleBasics,
            searchTitleBasics,
            searchInput
        }
    };
});
