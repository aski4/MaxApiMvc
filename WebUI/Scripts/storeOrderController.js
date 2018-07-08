var ordersUrl = "/nonrest/ordersapi";
var ordersListUrl = ordersUrl + "/list";
var ordersCreateUrl = ordersUrl + "/createorder/";
var ordersDeleteUrl = ordersUrl + "/deleteorder/";
var getAddressUrl = ordersUrl + "/address";
var getRoles = ordersUrl + "/roles";
var getUsersUrl = ordersUrl + "/users";

var getOrders = function () {
    sendRequest(ordersListUrl, "GET", null, function (data) {
        model.orders.removeAll();
        model.orders.push.apply(model.orders, data);
    });
}

var saveOrder = function (order, successCallback) {
    sendRequest(ordersCreateUrl, "POST", order, function () {
        if (successCallback) {
            successCallback();
        }
    });
}

var getAddress = function () {
    sendRequest(getAddressUrl, "GET", null, function (data) {
        model.address(data);
    });
}



var deleteOrder = function (id) {
    sendRequest(ordersDeleteUrl + id, "DELETE", null, function () {
        model.orders.remove(function (item) {
            return item.Id == id;
        })
    });
}

var getUsers = function () {
    sendRequest(getUsersUrl, "GET", null, function (data) {
        adminModel.users.removeAll();
        adminModel.users.push.apply(adminModel.users, data);
    });
}