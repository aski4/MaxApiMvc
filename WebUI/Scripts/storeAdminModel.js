var adminModel = {
    currentView: ko.observable("start"),
    secondView: ko.observable("start"),
    sequenceMy: ko.observable("start2"),
    listMode: ko.observable("products"),
    users: ko.observableArray([]),
    newProduct: { name: "" }
}