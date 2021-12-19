
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("list-searchHistory");

        let username = ko.observable();
        let password = ko.observable();

      

        let userLogin = () => {
            let user = {
                username: username(),
                password: password()
            };

            localStorage.removeItem("username");
            localStorage.removeItem("token");

            ds.userLogin(user, data => {
                console.log(data);
                localStorage.setItem("username", data.username);
                localStorage.setItem("token", data.token);
            });

            postman.publish("changeView", "list-titles");
        };

        let userLogin1 = () => {
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
