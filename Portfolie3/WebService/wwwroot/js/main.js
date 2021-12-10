/// <reference path="lib/jquery/dist/jquery.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />


require.config({
    baseUrl: 'js',
    paths: {
        jquery: "lib/jquery/dist/jquery.min",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        dataService: "services/dataService",
        text: "lib/requirejs/text",
        postman: "services/postman"
    }
});

require(['knockout'], (ko) => {
    ko.components.register("list-episodes", {
        viewModel: { require: "components/titleEpisodes/listTitleEpisodes" },
        template: { require: "text!components/titleEpisodes/listTitleEpisodes.html" }
    });

    ko.components.register("list-titles", {
        viewModel: { require: "components/titleBasics/listTitleBasics" },
        template: { require: "text!components/titleBasics/listTitleBasics.html" }
    });

    ko.components.register("single-title", {
        viewModel: { require: "components/titleBasics/singleTitleBasic" },
        template: { require: "text!components/titleBasics/singleTitleBasic.html" }
    });

    ko.components.register("list-comments", {
        viewModel: { require: "components/comments/listComments" },
        template: { require: "text!components/comments/listComments.html" }
    });

    ko.components.register("list-bookmarks", {
        viewModel: { require: "components/bookmarks/listBookmarks" },
        template: { require: "text!components/bookmarks/listBookmarks.html" }
    });

    ko.components.register("list-names", {
        viewModel: { require: "components/nameBasics/listNameBasics" },
        template: { require: "text!components/nameBasics/listNameBasics.html" }
    });

    ko.components.register("list-searchHistory", {
        viewModel: { require: "components/searchHistory/listSearchHistory" },
        template: { require: "text!components/searchHistory/listSearchHistory.html" }
    });

    ko.components.register("login", {
        viewModel: { require: "components/login/login" },
        template: { require: "text!components/login/login.html" }
    });

    ko.components.register("register", {
        viewModel: { require: "components/login/register" },
        template: { require: "text!components/login/register.html" }
    });
});





require(["knockout", "viewmodel"], function (ko, vm) {
    ko.applyBindings(vm);
});