define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");
        let ratings = ko.observableArray([]);

        let getRatingHistory = () => {
            console.log("getRatingHistory");
            ds.getRatingHistory(data => {
                console.log(data);
                ratings(data);
            });
            currentView("list");
        }


        return {
            currentComponent,
            currentView,
            ratings,
            getRatingHistory
        }
    };
});
