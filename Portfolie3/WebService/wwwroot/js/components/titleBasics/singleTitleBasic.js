
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");

        let posterUrl = ko.observable();
        let titleBasic = ko.observable();
        let selectId = ko.observable();
        let setRating = ko.observable();
        let titleId = ko.observable();

       
        

        let getInfo = (id) => {
            console.log("getInfo");
            ds.getTitleBasic(id, data => {
                console.log(data);
                titleBasic(data);
                posterUrl(data.poster);
                titleId(data.id);

            });
            ds.getRating("testuser",id, data => {
                console.log(data);
                console.log(data.setRating);

                setRating(data.rating);

            });
            currentView("list");
            selectId("");
        }

        postman.subscribe("getInfo", id => {
            console.log("postmanSubscribe")
            getInfo(id);
        }, "list-titles");


        postman.subscribe("newRating", rating=> {
            ds.addRating(rating, newRating => {
                console.log("postmanSubscribe")
            });
        }, "list-titles");

        let add = () => {
            console.log(setRating())
            setRating(parseInt(setRating()))
            postman.publish("newRating", { username: "testuser", titleId: titleId(), rating: setRating() });
            //postman.publish("changeView", "list-titles");
            popUpFunction();

        }

        let popUpFunction = () => {
            var popup = document.getElementById("myPopup");
            popup.classList.toggle("show");
            setTimeout(function () { popUpHideFunction(); }, 1500);

        }
        let popUpHideFunction = () => {
            var popup = document.getElementById("myPopup");
            popup.classList.toggle("show");

        }
        let addBookmark = (id) => {
            console.log(id);
            ds.createBookmark(id);
        }



       
        /*
        postman.subscribe("getTitle", id => {
            ds.getTitleBasics(id, getTitle => {
                console.log("postmanSubscribe")
            });
        }, "single-title");
        */

        let commentPage = (id) => {
            console.log(id);
            postman.publish("showComment", id);
            console.log("abe");

        }

        let changetoCommentView = (id) => {
            postman.publish("changeView", "list-comments");
            commentPage(id);
        }

        getInfo("tt0304141");

        return {
            currentComponent,
            popUpFunction,
            addBookmark,
            changetoCommentView,
            commentPage,
            currentView,
            titleBasic,
            getInfo,
            add,
            setRating,
            posterUrl
        }
    };
});
