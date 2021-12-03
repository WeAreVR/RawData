
define(["knockout", "postman"], function (ko, postman) {

    let currentView = ko.observable("list-episodes");

    postman.subscribe("changeView", function (data) {
        currentView(data);
    });

    return {
        currentView
    }
});







/* define(["knockout", "dataService"], function (ko, ds) {
    let currentComponent = ko.observable("list");
    
    let episodes = ko.observableArray([]);

    let currentView = ko.observable("list-episodes");



    let selectId = ko.observable();

    ds.getTitleEpisodes(selectId,data => {
        console.log(data);
        episodes(data);

    });

    let searchTitleEpisodes = () => {
        console.log("searchTitleEpisodes");
        ds.getTitleEpisodes(selectId(), data => {
            console.log(data);
            episodes(data);

        });
        currentView("list");
        selectId("");
    }

    return {
        currentComponent,
        currentView,
        episodes,
        searchTitleEpisodes,
        selectId
    }
});

*/