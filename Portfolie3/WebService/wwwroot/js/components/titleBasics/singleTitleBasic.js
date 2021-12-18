
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("single-title");

        let posterUrl = ko.observable();
        let titleBasic = ko.observable();
        let selectId = ko.observable();
        let setRating = ko.observable();
        let titleId = ko.observable();
        let comments = ko.observableArray([]);


       

        let getInfo = (id) => {
            console.log("getInfo");
            ds.getTitleBasic(id, data => {
                console.log(data);
                titleBasic(data);
                posterUrl(data.poster);
                titleId(data.id);
                console.log(titleId());


            });
            ds.getRating(localStorage.getItem("username"),id, data => {
                console.log(data);
                console.log(data.setRating);
                comments(data);
                setRating(data.rating);

            });
            ds.getComments(id, data => {
                console.log(data);
                comments(data);


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
            popUpFunction(1);

        }

        let popUpFunction = (id) => {
            var popup = document.getElementById("myPopup"+id);
            popup.classList.toggle("show");
            setTimeout(function () { popUpHideFunction(id); }, 1500);

        }
        let popUpHideFunction = (id) => {
            var popup = document.getElementById("myPopup"+id);
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

        }

        let changetoCommentView = (id) => {
            postman.publish("changeView", "list-comments");
            postman.publish("showComment", id());

        }
        let toggleBookmark = (id) => {
            ds.toggleBookmark(id);
            popUpFunction(2);

        }

        return {
            currentComponent,
            toggleBookmark,
            crewPage,
            titleId,
            crewView,
            popUpFunction,
            addBookmark,
            changetoCommentView,
            commentPage,
            currentView,
            titleBasic,
            getInfo,
            add,
            comments,
            setRating,
            posterUrl
        }
    };
});
