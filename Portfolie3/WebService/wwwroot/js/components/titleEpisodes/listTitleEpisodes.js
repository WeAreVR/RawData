
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let titleEpisodes = ko.observableArray([]);

     
        return {
            titleEpisodes,
        };
    };
});
