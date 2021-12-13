
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
            postman.publish("newRating", { username: "testuser", titleId: titleId(), rating: setRating() });
            postman.publish("changeView", "list-titles");
        }

       
        let commentSection = () => postman.publish("changeView", "list-comments");


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
