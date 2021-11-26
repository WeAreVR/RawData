define(["knockout", "dataService"], function (ko, ds) {
    let currentView = ko.observable("list");

    let categories = ko.observableArray([]);

    let deleteCategory = category => {
        console.log(category);
        categories.remove(category);
        ds.deleteCategory(category);
    }

    let selectName = ko.observable();
    let selectDescription = ko.observable();

    let addCategory = () => {
        console.log("addCategory");
        let category = { name: selectName(), description: selectDescription() };
        ds.createCategory(category, newCategory => {
            categories.push(newCategory);
        });
        currentView("list");
        selectName("");
        selectDescription("");
    }

    let addCategoryView = () => currentView("add");

    let cancelAddCategory = () => currentView("list");


    ds.getCategories(data => {
        console.log(data);
        categories(data);
    });

    return {
        currentView,
        addCategoryView,
        cancelAddCategory,
        categories,
        deleteCategory,
        addCategory,
        selectName,
        selectDescription
    }
});