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

});





require(["knockout", "viewmodel"], function (ko, vm) {
    //console.log(vm.firstName);

    ko.applyBindings(vm);

});