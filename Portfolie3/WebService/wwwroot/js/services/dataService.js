define([], () => {

    const titleBasicApiUrl = "api/titlebasic/search/";
    const searchHistoryApiUrl = "api/searchHistory/";
    const bookmarkApiUrl = "api/bookmark/";

    //TitleEpisodes
    let getTitleEpisodes = (id,callback) => {
        fetch("api/titleepisode/allepisodes/"+ "?parentTitleId=" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    //TitleBasics
    let getTitleBasics = (searchInput, callback) => {
        fetch("api/titlebasic/search/" + "?searchInput=" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getTitleBasic = (id, callback) => {
        fetch("api/titlebasic/" + id)
            .then(response => response.json())
            .then(json => callback(json));

    };

    let getUrl = (url, callback) => {
        fetch(url)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getComments = (id, callback) => {
        fetch("api/comments/" + "?titleId=" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let updateComment = (comment, callback) => {
        let param = {
            method: "PUT",
            body: JSON.stringify(comment),
            headers: {
                "Content-Type": "application/json"
            }
        }
        console.log(param)
        fetch("api/comments", param)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let addComment = (comment, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(comment),
            headers: {
                "Content-Type": "application/json"
            }
        }
        console.log(param)
        fetch("api/comments", param)
            .then(response => response.json())
            .then(json => callback(json));
    };

  
    //addRating
    let addRating = (rating, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(rating),
            headers: {
                "Content-Type": "application/json"
            }
        }
        console.log(param)
        fetch("api/ratinghistory", param)
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
    let getSearchHistory = () => {
        let param = {
            method: "GET",
            headers: {
                "Authorization": "Barer " + localStorage.getItem("token")
            }
        };
        return fetch("api/searchhistory", param)
            .then(response => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }
                response.json();
            });
    };

    let getSearchHistory1 = (callback) => {
        return fetch("api/searchhistory/?username=" + localStorage.getItem("username"))
            .then(response => response.json())
            .then(json => callback(json));
            };
    

    let getSearchHistoryUrl = (url, callback) => {
        fetch(url)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let clearSearch = username => {
        fetch(searchHistoryApiUrl + username, { method: "DELETE" })
            .then(response => console.log(response.status))
    }

    let getSearchHistoryWithPageSize = (currentPage, pageSize) => searchHistoryApiUrl + "?page=" + currentPage + "&pagesize=" + pageSize;

    let getTitleBasicsWithPageSize = (searchInput, currentPage, pageSize) => titleBasicApiUrl + searchHistoryApiUrl + "?searchInput=" + searchInput + "&page=" + currentPage + "&pagesize=" + pageSize;

    //login and register
    let userRegister = (user, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                "Content-Type": "application/json"
            }
        };
        fetch("api/users/register", param)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let userLogin = (user, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                "Content-Type": "application/json"
            }
        }
        fetch("api/users/login", param)
            .then(response => response.json())
            .then(json => callback(json));
    };





    //Bookmark skal skrives om til at kunne tage username
    let getBookmarks = (callback) => {
        fetch("api/bookmark")
            .then(response => response.json())
            .then(json => callback(json));
    };

    let createBookmark1= (bookmark, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(bookmark),
            headers: {
                "Content-Type": "application/json"
            }
        }
        fetch("api/bookmark", param)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let createBookmark = id => {
        fetch(bookmarkApiUrl + id, { method: "POST" })
            .then(response => console.log(response.status))
    }


    //http://localhost:5001/api/titlebasic/search?searchInput=5&page=1&pageSize=10
    return {
        getTitleEpisodes,
        getUrl,
        getTitleBasics,
        getComments,
        addComment,
        getTitleBasicsWithPageSize,
        getNameBasics,
        getSearchHistory,
        getSearchHistoryUrl,
        getSearchHistoryWithPageSize,
        getTitleBasic,
        userRegister,
        userLogin,
        getBookmarks,
        clearSearch,
        addRating,
        createBookmark,
        updateComment
    }
});