/// <reference path="lib/jquery/dist/jquery.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />


require.config({
    baseUrl: 'js',
    paths: {
        jquery: "lib/jquery/dist/jquery.min",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        dataService: "services/dataService"
    }
});


require(["knockout", "viewmodel"], function (ko, vm) {
    //console.log(vm.firstName);

    ko.applyBindings(vm);

});