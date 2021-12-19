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

    //Comments

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

    let deleteComment = comment => {
        let param = {
            method: "DELETE",
            body: JSON.stringify(comment),
            headers: {
                "Content-Type": "application/json"
            }
        }
        console.log(param)
        fetch("api/comments", param)
            .then(response => console.log(response.status))
    };

  
    //Rating
    let addRating = (rating, callback) => {
        let param = {
            method: "PUT",
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
    
    let getRating = (username, id, callback) => {
        fetch("api/ratinghistory/" + "?username=" + username + "&titleId=" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };

    //nameBasics
    let getNameBasics = (searchInput, callback) => {
        fetch("api/namebasic/search/" + "?searchInput=" + searchInput)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getNameBasic = (id, callback) => {
        fetch("api/namebasic/" + id)
            .then(response => response.json())
            .then(json => callback(json));
    };


    //searchHistory;
    let getSearchHistory = () => {
        let params = {
            method: "GET",
            headers: {
                "Authorization": "Barer " + localStorage.getItem("token")
               
            }
        };
        return fetch("api/searchhistory/?username=" + localStorage.getItem("username"), params)
            .then(response => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }
                return response.json();
            });
    }


    let getSearchHistoryUrl = (url, callback) => {
        fetch(url)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let clearSearch = () => {
        fetch(searchHistoryApiUrl + localStorage.getItem("username"), { method: "DELETE" })
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




    let getBookmarks = () => {
        let params = {
            method: "GET",
            headers: {
                "Authorization": "Barer " + localStorage.getItem("token")
            }
        };
        return fetch("api/bookmark/?username=" + localStorage.getItem("username"), params)
            .then(response => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }
                return response.json();
            });
    }

   

    let toggleBookmark = (id) => {
        console.log(id);
        console.log(localStorage.getItem("username"));

            console.log("bookmark createdddd!");
            fetch("api/bookmark/toggle/?username=" + localStorage.getItem("username") + "&titleid=" + id)
            .then(response => console.log(response.status))
        }

    let createBookmark = (id) => {
        console.log(checkBookmarks(id));
        if (checkBookmarks(id) == false)
        {
            console.log("bookmark created!");
            fetch(bookmarkApiUrl + "?username=" + localStorage.getItem("username") + "&titleId=" + id,
                { method: "POST" })
                .then(response => console.log(response.status))
        }
        else {
            console.log("bookmark deleted!");
            fetch(bookmarkApiUrl + "?username=" + localStorage.getItem("username") + "&titleId=" + id,
                { method: "DELETE" })
                .then(response => console.log(response.status))

        }
        
    }


    //http://localhost:5001/api/titlebasic/search?searchInput=5&page=1&pageSize=10
    return {
        getTitleEpisodes,
        getUrl,
        getTitleBasics,
        getComments,
        addComment,
        deleteComment,
        getTitleBasicsWithPageSize,
        getNameBasics,
        getNameBasic,
        getSearchHistory,
        getSearchHistoryUrl,
        getSearchHistoryWithPageSize,
        getTitleBasic,
        userRegister,
        userLogin,
        getBookmarks,
        clearSearch,
        addRating,
        getRating,
        createBookmark,
        toggleBookmark,
        updateComment
    }
});