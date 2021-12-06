define([], () => {

    let getTitleEpisodes = (id,callback) => {
        fetch("api/titleepisode/allepisodes/"+ id)
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getTitleBasics = (searchInput, callback) => {
        fetch("api/titlebasic/search/" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getComments = (id, callback) => {
        fetch("api/comment/" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    return {
        getTitleEpisodes,
        getTitleBasics,
        getComments
    }
});