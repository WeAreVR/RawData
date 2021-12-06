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

    ko.components.register("list-comments", {
        viewModel: { require: "components/comments/listComments" },
        template: { require: "text!components/comments/listComments.html" }
    });
});





require(["knockout", "viewmodel"], function (ko, vm) {
    ko.applyBindings(vm);
});