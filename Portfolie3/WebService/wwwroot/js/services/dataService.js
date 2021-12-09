define([], () => {

    const titleBasicApiUrl = "api/titlebasic/search/";
    const searchHistoryApiUrl = "api/searchHistory/";

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

    //nameBasics
    let getNameBasics = (searchInput, callback) => {
        fetch("api/namebasic/search/" + "?searchInput=" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };


    //searchHistory
    let getSearchHistoryUrl = (url, callback) => {
        fetch(url)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getSearchHistoryWithPageSize = (currentPage, pageSize) => searchHistoryApiUrl + "?page=" + currentPahe + "&pagesize=" + pageSize;

    let getTitleBasicsWithPageSize = (searchInput, currentPage, pageSize) => titleBasicApiUrl + searchHistoryApiUrl + "?searchInput=" + searchInput + "&page=" + currentPahe + "&pagesize=" + pageSize;


    //http://localhost:5001/api/titlebasic/search?searchInput=5&page=1&pageSize=10
    return {
        getTitleEpisodes,
        getTitleBasicsUrl,
        getTitleBasics,
        getComments,
        getTitleBasicsWithPageSize,
        getNameBasics,
        getSearchHistoryUrl,
        getSearchHistoryWithPageSize
    }
});