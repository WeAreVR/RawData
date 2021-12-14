
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("single-names");

        let nameBasic = ko.observableArray([]);
        let selectId = ko.observable();
        let setRating = ko.observable();
        let birthYear = ko.observable();
        let deathYear = ko.observable();
        let nameId = ko.observable();


        let getInfo = (id) => {
            console.log("getInfo");
            ds.getNameBasic("nm0000001 ", data => {
                console.log(data);
                nameBasic(data);
                birthYear(data.birthYear);
                deathYear(data.deathYear);
                setRating(data.rating);
                console.log(birthYear());
                console.log(deathYear());
                nameId(data.id);
                console.log(nameId());
            });
            currentView("list");
            selectId("");
        }

        postman.subscribe("getInfoForSingleName", id => {
            console.log("postmanSubscribe");
            getInfo(id);
        }, "list-titles");


        let searchNameBasics = () => {
            console.log("searchNameBasics");
            ds.getNameBasics(selectId(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasics(data);
            });
            currentView("list");
            selectId("");
        }

        
        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            searchNameBasics(ds.getNameBasicsWithPageSize(size));
        });
        

        return {
          
            selectedPageSize,
            setRating,
            birthYear,
            deathYear,
            nameId,
            currentComponent,
            currentView,
            nameBasic,
            searchNameBasics,
            selectId
        }
    };
});
