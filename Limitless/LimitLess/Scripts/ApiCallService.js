(function () {
    var app = angular
     .module('app');
    app.service("ApiCall", ["$http", function ($http) {
        var result;
        $http.defaults.useXDomain = true;
        delete $http.defaults.headers.common['X-Requested-With'];
        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }
        // This is used for calling get methods from web api
        this.Get = function (url) {
            var serviceuri = "http://localhost:60244";
            result = $http.get(serviceuri + url, config).success(function (data, status) {
                result = (data);
            }).error(function () {
                alert("Something went wrong");
            });
            return result;
        };

        // This is used for calling post methods from web api with passing some data to the web api controller
        this.PostApiCall = function (url, reqdata) {
            var serviceuri = "http://localhost:60244";
            result = $http.post(serviceuri + url, reqdata, config)
             .success(function (data, status) {
                 result = (data);
             })
             .error(function (error, status, header, config) {
                 alert("Something went wrong");
             });
            
            return result;
        };
    }]);
})();
