/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("UserController", function ($scope, $http, $rootScope) {
    $http.get("api/Organization/GetList", {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Organizations = JSON.parse(data.List);
    });
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'UserName',
        SortDirection: 'ASC',
    });
    $http.post("api/User/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });
    $scope.clear = function () {
        $scope.UserName = '';
        $scope.FName = '';
        $scope.LName = '';
        $scope.OrganizationID = '';
        $scope.Password = '';
        $scope.Email = '';
        $scope.UserID = '';
        $('#chkActive').attr('checked', false);
    };
    var IsEdit='';
    $scope.EditUser = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        IsEdit='Y';
        $scope.SubjectID = id;
        $http.post("api/User/GetUserDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.UserName = result[0].UserName;
            $scope.FName = result[0].FName;
            $scope.LName = result[0].LName;
            $scope.OrganizationID = result[0].OrganizationID;
            $scope.Password = result[0].OrganizationID;
            $scope.Email = result[0].Email;
            $scope.UserID = result[0].UserID;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    
    $scope.SaveUser =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        if ($scope.UserName=='') {
            toastr["Please Enter User Name"]('', 'Validation');
        }
        else if ($scope.FName=='') {
            toastr["Please Enter First Name"]('', 'Validation');
        }
        else if ($scope.LName == '') {
            toastr["Please Enter Last Name"]('', 'Validation');
        }
        else if ($scope.OrganizationID == '') {
            toastr["Please Select Orgasnization"]('', 'Validation');
        }
        else if ($scope.Password == '' && IsEdit=='') {
            toastr["Please Enter Password"]('', 'Validation');
        }
        else if ($scope.Email == '') {
            toastr["Please Enter Email ID"]('', 'Validation');
        }
        else {
            var Postdata = $.param({
                UserName: $scope.UserName,
                FName: $scope.FName,
                LName: $scope.LName,
                OrganizationID: $scope.OrganizationID,
                Password: $scope.Password,
                Email: $scope.Email,
                UserID: $scope.UserID,
                IsActive: IsActive,
            });
            $http.post("api/User/SaveUser", Postdata, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Saved Successfully.", 'Create Subject');
                $http.post("api/User/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
                //$location.path('/subjects');
            }).error(function (error) {
                toastr["error"](error.error_message, 'Create Subject');
            });
        }
    };
});
