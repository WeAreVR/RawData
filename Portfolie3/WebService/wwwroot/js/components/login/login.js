
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-searchHistory");

        let username = ko.observable();
        let password = ko.observable();

        postman.subscribe("newUser", user => {
            ds.userLogin(user, newUser => {
                console.log("postmanSubscribe")
            });
        }, "list-titles");

        let userLogin = () => {
            postman.publish("newUser", { username: username(), password: password() });
            console.log("postmanPublish");
            postman.publish("changeView", "list-titles");
        }

        let registerPage = () => {
            console.log("registerPage");
            postman.publish("changeView", "register");
        };
        

        return {
            currentComponent,
            currentView,
            userLogin,
            username,
            password,
            registerPage
        }
    };
});
