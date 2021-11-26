define([], () => {

    let getTitleEpisodes = (id,callback) => {
        fetch("api/titleepisode/allepisodes/"+ id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    

    return {
        getTitleEpisodes
    }
});