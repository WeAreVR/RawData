
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("single-title");

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
            ds.getRating(localStorage.getItem("username"),id, data => {
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
            postman.publish("newRating", { username: localStorage.getItem("username"), titleId: titleId(), rating: setRating() });
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

        let crewPage = () => {
            console.log(id);
            postman.publish("getInfo", titleId());
            console.log("abe");

        }

        let crewView = () => {
            postman.publish("changeView", "list-crew");
            //singleTitlePage(id);
            postman.publish("getCrewInfo", titleId());


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

        return {
            currentComponent,
            crewPage,
            crewView,
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
