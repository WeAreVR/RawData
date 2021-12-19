
define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let currentComponent = ko.observable("list");
        let pageSize = [5, 10, 15, 100];
        let selectedPageSize = ko.observableArray([10]);
        let currentView = ko.observable("list-plays");
        let prev = ko.observable();
        let next = ko.observable();
        let listPlay = ko.observableArray([]);
        let nameId = ko.observable();
        let name = ko.observable();



        let nameBasic = ko.observableArray([]);

        let selectId = ko.observable();
      
        let enablePrev = ko.observable(() => prev() !== undefined);

        let showNext = () =>
        {
            console.log(next());
            ds.getUrl(next(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasic(data);

            });
        }
        let showPrev = () => {
            console.log(next());
            ds.getUrl(prev(), data => {
                console.log(data);
                prev(data.prev),
                next(data.next),
                nameBasic(data);
             
            });
        }

        let enableNext = ko.observable(() => next() !== undefined);

        let getInfo = (id) => {
            console.log("getInfo");
            ds.getNameBasic(id, data => {
                console.log(data);
                nameBasic(data);
                nameId(data.id);
                name(data.primaryName);
                console.log(nameId());
                listPlay(data.listPlays);
                console.log(listPlay());

            });
            currentView("list");
            selectId("");
        }


        postman.subscribe("getInfoForPlays", id => {
            console.log("postmanSubscribe");
            getInfo(id);
        }, "list-titles");

        
        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            searchNameBasics(ds.getNameBasicsWithPageSize(size));
        });
        
        



        let changeSingleNameView = (id) => {
            postman.publish("changeView", "single-names");
           // singleTitlePage(id);
            postman.publish("getInfoForSingleName", id);

        }
        //den hedder single-names skal ændres til bare name
        let goBack = () => {
            postman.publish("changeView", "single-names");
            postman.publish("getInfoForSingleName", nameId());

        }

        return {
            enableNext,
            goBack,
            changeSingleNameView,
            enablePrev,
            showNext,
            showPrev,
            prev,
            next,
            name,
            selectedPageSize,
            pageSize,
            currentComponent,
            currentView,
            nameBasic,
            listPlay,
            selectId
        }
    };
});
