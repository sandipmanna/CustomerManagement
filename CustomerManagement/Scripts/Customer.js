
function CustomerViewModelData($scope, $http) {
    $scope.CustomerViewModel = {
        "Customer": {
            "CustomerCode": "",
            "CustomerName": "",
            "CusotmerAmount": "",
            "CustomerAmountColor": ""
        }
    };
    $scope.Customers = {};
    $scope.$watch("Customers", function () {
        for (var i = 0; i < $scope.Customers.length; i++) {
            var cust = $scope.Customers[i];
            cust.CustomerAmountColor = $scope.getColor(cust.CustomerAmount);
        }
    })
    $scope.getColor = function (amount) {
        if (amount == undefined)
            return "";
        else if (amount > 100)
            return "green";
        else
            return "red";
    };

    $scope.$watch("CustomerViewModel.Customer.CustomerAmount", function () {
        $scope.CustomerViewModel.Customer.CustomerAmountColor
            = $scope.getColor($scope.CustomerViewModel.Customer.CustomerAmount);
    })

    $scope.AddCustomer = function () {
        $http({ method: 'POST', data: $scope.CustomerViewModel, url: "AddCustomer" }).
        then(function (response) {
            $scope.Customers = response.data;
            $scope.CustomerViewModel = {
                "Customer": {
                    "CustomerCode": "",
                    "CustomerName": "",
                    "CusotmerAmount": "",
                    "CustomerAmountColor": ""
                }
            }
        })
    };

    $scope.LoadData = function () {
        $http({ method: 'GET', url: "GetCustomer" }).
        then(function (response) {
            $scope.Customers = response.data;
        })
    };
    $scope.LoadDataByCode = function () {
        var CustSearch = $scope.CustomerViewModel;
        $http({ method: 'GET', params: { CustomerCode: $scope.CustomerViewModel.Customer.CustomerCode }, url: "CustomerByCode" }).
        then(function (response) {
            $scope.Customers = response.data;
        })
    };
    $scope.LoadData();
}
