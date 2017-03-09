/// <reference path="angular.js" />
/// <reference path="angular-cookies.js" />
var app = angular.module("app", ['ngRoute', 'ngCookies', 'ngFileUpload', ]).run(run);

//config routing
app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "/Modules/Organizations/Views/Index.html",
        controller: "OrganizationController"
    })
    .when("/login", {
        templateUrl: "/Index.html",
        controller: "loginController"
    })
    .when("/users", {
        templateUrl: "/Modules/User/Views/Index.html",
        controller: "UserController"
    })
    .when("/subjects", {
        templateUrl: "/Modules/Subjects/Views/Index.html",
        controller: "SubjectController"
    }).when("/CreateSubject", {
        templateUrl: "/Modules/Subjects/Views/CreateSubject.html",
        controller: "SubjectController"
    })
    .when("/EditSubject", {
        templateUrl: "/Modules/Subjects/Views/EditSubject.html",
        controller: "SubjectController"
    })
    .when("/lessons", {
        templateUrl: "/Modules/Lessons/Views/Index.html",
        controller: "LessonController"
    }).when("/CreateLesson", {
        templateUrl: "/Modules/Lessons/Views/CreateLesson.html",
        controller: "LessonController"
    })
    .when("/EditLesson", {
        templateUrl: "/Modules/Lessons/Views/EditLesson.html",
        controller: "LessonController"
    })
    .when("/subobjectives", {
        templateUrl: "/Modules/Subobjectives/Views/Index.html",
        controller: "SubObjectiveController"
    })
    .when("/CreateSubObjective", {
        templateUrl: "/Modules/Subobjectives/Views/CreateSubOject.html",
        controller: "SubObjectiveController"
    })
    .when("/EditSubObjective", {
        templateUrl: "/Modules/Subobjectives/Views/EditSubject.html",
        controller: "SubObjectiveController"
    })
    .when("/questions", {
        templateUrl: "/Modules/Questions/Views/Index.html",
        controller: "QuestionController"
    })
    .when("/AddQuestion", {
        templateUrl: "/Modules/Questions/Views/CreateQuestion.html",
        controller: "QuestionController"
    })
    .when("/EditQuestion", {
        templateUrl: "/Modules/Questions/Views/EditQuestion.html",
        controller: "QuestionController"
    })
    .when("/answers", {
        templateUrl: "/Modules/Answers/Views/Index.html",
        controller: "AnswerController"
    })
    .when("/objectives", {
        templateUrl: "/Modules/Objectives/Views/Index.html",
        controller: "ObjectiveController"
    })
    .when("/CreateObjective", {
        templateUrl: "/Modules/Objectives/Views/AddObjectives.html",
        controller: "ObjectiveController"
    })
    .when("/EditObjective", {
        templateUrl: "/Modules/Objectives/Views/EditObjectives.html",
        controller: "ObjectiveController"
    })
    .when("/topics", {
        templateUrl: "/Modules/Topics/Views/Index.html",
        controller: "TopicController"
    })
    .when("/CreateTopic", {
        templateUrl: "/Modules/Topics/Views/CreateTopic.html",
        controller: "TopicController"
    })
    .when("/EditTopic", {
        templateUrl: "/Modules/Topics/Views/EditTopic.html",
        controller: "TopicController"
    })
    .when("/organization", {
        templateUrl: "/Modules/Organizations/Views/Index.html",
        controller: "OrganizationController"
    })
    .when("/CreateOrganization", {
        templateUrl: "/Modules/Organizations/Views/AddOrganization.html",
        controller: "OrganizationController"
    })
    .when("/EditOrganization", {
        templateUrl: "/Modules/Organizations/Views/EditOrganization.html",
        controller: "OrganizationController"
    })
    .when("/spreadsheet", {
        templateUrl: "/Modules/Spreadsheet/Views/Index.html",
        controller: "SpreadsheetController"
    })
});

//config routing

//global variable to store service base path
app.constant('serviceBasePath', 'http://limitless.saasitsol.com');
//global variable to store service base path

//Login and Logout controller
//Login and Logout controller

//Global Services
app.factory('UserDataService', function () {
    var fac = {};
    fac.SetUser = function (name) {
        fac.CurrentName = name;
        sessionStorage.Name = user;
    }
    fac.GetUser = function () {
        fac.CurrentName = sessionStorage.Name;
        return fac.currentUser;
    }
    return fac;
})
app.factory('dataService', ['$http', 'serviceBasePath', function ($http, serviceBasePath) {
    var fact = {};
    fact.GetAnonymousData = function () {
        return $http.get(serviceBasePath + 'api/data/AnonymousUser').then(function (response) {
            return response.data;
        })
    }
    fact.GetAuthenticatedData = function () {
        return $http.get(serviceBasePath + 'api/data/AuthenticatedUser').then(function (response) {
            return response.data;
        })
    }
    fact.GetAuthorizedData = function () {
        return $http.get(serviceBasePath + 'api/data/AuthorizedUser').then(function (response) {
            return response.data;
        })
    }
    return fact;
}])
app.factory('userService', function () {
    var fac = {};
    fac.SetCurrentUser = function (user) {
        fac.currentUser = user;
        sessionStorage.user = angular.toJson(user);
    }
    fac.GetCurrentUser = function () {
        fac.currentUser = angular.fromJson(sessionStorage.user);
        return fac.currentUser;
    }
    fac.GetName = function () {
        fac.Name = sessionStorage.Name;
        return fac.Name;
    }
    fac.SetName = function (name) {
        sessionStorage.Name = name;
    }
    return fac;
})
app.factory('accountService', ['$http', '$q', 'serviceBasePath', 'userService', function ($http, $q, serviceBasePath, userService) {
    var fac = {};
    fac.login = function (username, password) {
        var obj = { 'username': username, 'password': password, 'grant_type': 'password' };
        Object.toparams = function ObjectToParams(obj) {
            var p = [];
            for (var key in obj) {
                p.push(key + '=' + encodeURIComponent(obj[key]));
            }
            return p.join('&');
        }
        var defer = $q.defer();
        $http({
            method: 'post',
            url: serviceBasePath + '/token',
            data: Object.toparams(obj),
            headers: { 'content-type': 'application/x-www-form-urlencoded' }
        }).then(function (response) {
            defer.resolve(response.data);
            userService.SetCurrentUser(response.data);
            sessionStorage.access_token = response.data.access_token;
        }, function (error) {
            defer.reject(error.data);
        })
        return defer.promise;
    }
    fac.logout = function () {
        userService.CurrentUser = null;
        userService.SetCurrentUser(userService.CurrentUser);
        sessionStorage = null;
    }
    return fac;
}])
app.controller('loginController', ['$scope', '$location', 'accountService', '$timeout', '$cookieStore', '$http', function ($scope, $location, accountService, $timeout, $cookieStore, $http) {
    $scope.ChangeData = function () {
        $scope.password = calcSHA1($scope.pwd);
    }
    $scope.account = { username: $scope.username, password: $scope.password };
    $scope.message = '';
    $scope.login = function () {
        if ($scope.username == undefined) {
            toastr["error"]('Please Enter User Id', 'User Authentication');
        }
        else if ($scope.pwd == undefined) {
            toastr["error"]('Please Enter Password', 'User Authentication');
        }
        else {
            var req_data = $.param({
                Email: $scope.username,
                Password: $scope.password,
                UserType:"2"
            });
            $http.post("api/Login/GetUserData", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                $scope.User_Details = JSON.parse(data.UserInformations);
                if ($scope.User_Details == null) {
                    toastr["error"]('Please Enter Valid Credential', 'User Authentication');
                }
                else {
                    $cookieStore.put('UserName', $scope.User_Details[0].UserName);
                    $cookieStore.put('OrganizationID', $scope.User_Details[0].OrganizationID);
                    $cookieStore.UserName = $scope.User_Details[0].UserName;
                    $cookieStore.UserID = $scope.User_Details[0].UserID;
                    $cookieStore.OrganizationID = $scope.User_Details[0].OrganizationID;
                    $cookieStore.put('UserData', $scope.User_Details);
                    accountService.login($scope.username, $scope.password).then(function (data) {
                        toastr["success"]("User loggedin successfully.", 'User Authentication');
                        $timeout(LoginTime, 1000);
                    }, function (error) {
                        toastr["error"](error.error_description, 'User Authentication');
                        $scope.message = error.error_description;
                    })
                }
            }).error(function (err) {
            });           
        }
    }
    $scope.Logout = function () {
        toastr["warning"]("User loggedout successfully.", 'User Authentication');
        accountService.logout();
        $timeout(LogOutTime, 1000);
        $cookieStore = null;
    }
}])
function LoginTime() {
    window.location = 'dashboard.html';
}
function LogOutTime() {
    window.location = 'index.html';
}
//Global Services

//http interceptor
app.config(['$httpProvider', function ($httpProvider) {
    var interceptor = function (userService, $q, $location) {
        return {
            request: function (config) {
                var currentUser = userService.GetCurrentUser();
                if (currentUser != null) {
                    //config.headers['Authorization'] = 'Bearer ' + sessionStorage.access_token;
                    config.headers.authorization = 'Bearer ' + sessionStorage.access_token;
                }
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    toastr["warning"](rejection.statusText, 'Invalid access');
                    window.location = 'index.html';
                    return $q.reject(rejection);
                }
                if (rejection.status === 403) {
                    toastr["warning"](rejection.statusText, 'Invalid access');
                    window.location = 'index.html';
                    return $q.reject(rejection);
                }
                return $q.reject(rejection);
            }
        }
    }
    var params = ['userService', '$q', '$location'];
    interceptor.$inject = params;
    $httpProvider.interceptors.push(interceptor);
}])
//http interceptor


run.$inject = ["$rootScope", "$cookieStore", "$http"];
function run($rootScope, $cookieStore, $http) {
    $rootScope.UserData = $cookieStore.get("UserData") || {};
}

