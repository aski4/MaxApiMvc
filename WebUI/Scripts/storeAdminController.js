var setView = function (view) {
    adminModel.currentView(view);
}

var setView2 = function (view2) {
    adminModel.secondView(view2);
    
}

var setListMode = function (mode) {
    console.log("Mode: " + mode);
    adminModel.listMode(mode);
    getUsers();
}

var authenticateUser = function () {
    authenticate(function () {
        setView("productList");
        getProducts();
        getOrders();

    })
}

var createProduct = function () {
    saveProduct(adminModel.newProduct, function () {
        setListMode("start");
    })
}

var removeProduct = function (product) {
    deleteProduct(product.ProductId);
    setView("productList");
}

var removeOrder = function (order) {
    deleteOrder(order.Id);
}


