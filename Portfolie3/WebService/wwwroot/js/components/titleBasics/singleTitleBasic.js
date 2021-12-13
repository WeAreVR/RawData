
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-titles");

        let posterUrl = ko.observable();
        let titleBasic = ko.observable();
        let selectId = ko.observable();
        let setRating = ko.observable();

       
        

        let getInfo = () => {
            console.log("getInfo");
            ds.getTitleBasic("tt11503082", data => {
                console.log(data);
                titleBasic(data);
                posterUrl(data.poster);
            });
            currentView("list");
            selectId("");
        }


        postman.subscribe("newRating", rating=> {
            ds.addRating(rating, newRating => {
                console.log("postmanSubscribe")
            });
        }, "list-titles");

        let add = () => {
            console.log(setRating())
            postman.publish("newRating", { username: "testuser", titleId: "tt0312280", rating: setRating() });
            postman.publish("changeView", "list-titles");
        }

       
        let commentSection = () => postman.publish("changeView", "list-comments");

        getInfo();

        return {
            currentComponent,
            currentView,
            titleBasic,
            getInfo,
            add,
            commentSection,
            setRating,
            posterUrl
        }
    };
});
