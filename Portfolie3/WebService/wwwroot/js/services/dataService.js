define([], () => {

    const titleBasicApiUrl = "api/titlebasic";
    let getTitleEpisodes = (id,callback) => {
        fetch("api/titleepisode/allepisodes/"+ "?parentTitleId=" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getTitleBasics = (searchInput, callback) => {
        fetch("api/titlebasic/search/" + "?searchInput=" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getComments = (id, callback) => {
        fetch("api/comment/" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

   // let getTitleBasicsWithPageSize = size => titleBasicApiUrl + "?pageSize=" + size;

    return {
        getTitleEpisodes,
        getTitleBasics,
        getComments
       // getTitleBasicsWithPageSize
    }
});