
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let titleBasics = ko.observableArray([]);

     
        return {
            titleBasics,
        };
    };
});
