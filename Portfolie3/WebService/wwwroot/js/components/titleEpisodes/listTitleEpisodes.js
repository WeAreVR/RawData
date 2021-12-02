
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let titleEpisodes = ko.observableArray([]);

     
        let create = () => postman.publish("changeView", "add-category");

        ds.getCategories(categories);

        postman.subscribe("newCategory", category => {
            ds.createCategory(category, newCategory => {
                categories.push(newCategory);
            });
        }, "list-categories");

        return {
            categories,
            del,
            create
        };
    };
});
