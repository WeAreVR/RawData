define([], () => {

    let getTitleEpisodes = (id,callback) => {
        fetch("api/titleepisode/allepisodes/"+ id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getTitleBasicsBySearch = (searchInput,callback) => {
        fetch("api/titlebasic/search/" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };
    

    return {
        getTitleEpisodes,
        getTitleBasicsBySearch
    }
});