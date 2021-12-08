define([], () => {

    const titleBasicApiUrl = "api/titlebasic/search";

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
    let getTitleBasicsUrl = (url, callback) => {
        fetch(url)
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getComments = (id, callback) => {
        fetch("api/comment/" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getTitleBasicsWithPageSize = size => titleBasicApiUrl +  "?pageSize=" + size;
    //http://localhost:5001/api/titlebasic/search?searchInput=5&page=1&pageSize=10
    return {
        getTitleEpisodes,
        getTitleBasicsUrl,
        getTitleBasics,
        getComments,
       getTitleBasicsWithPageSize
    }
});