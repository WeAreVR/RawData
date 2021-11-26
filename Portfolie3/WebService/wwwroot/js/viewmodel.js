define(["knockout", "dataService"], function (ko, ds) {
    let currentView = ko.observable("list");
    
    let episodes = ko.observableArray([]);


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
        currentView,
        episodes,
        searchTitleEpisodes,
        selectId
    }
});