
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let currentView = ko.observable("register");

        let username = ko.observable();
        let password = ko.observable();



        let userRegister = () => {
            postman.publish("newUser", { uname: username(), psw: password() });
            postman.publish("changeView", "login");
        }

        let loginPage = () => {
            console.log("loginPage");
            postman.publish("changeView", "login");
        };

        return {
            currentComponent,
            currentView,
            userRegister,
            loginPage,
            username,
            password
        }
    };
});
