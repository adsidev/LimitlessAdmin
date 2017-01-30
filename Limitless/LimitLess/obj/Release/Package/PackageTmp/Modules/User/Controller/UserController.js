/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("UserController", function ($scope, $http, $rootScope, $cookieStore) {
    $scope.ChangeData = function () {
        $scope.Password = calcSHA1($scope.pwd);
    }
    var ListInput = $.param({
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/Organization/GetList", ListInput, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Organizations = JSON.parse(data.List);
    });
    
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'UserName',
        SortDirection: 'ASC',
        OrganizationID: $cookieStore.get('OrganizationID')
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
        $scope.pwd = '';
        $scope.Email = '';
        $scope.UserID = '';
        $('#chkActive').attr('checked', false);
        $scope.EditHide = false;
    };
    $scope.DeleteUser = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        if (confirm('Are you sure you want to delete record?')) {
            $http.post("api/User/DeleteUser", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Deleted Successfully.", 'Delete User');
                $http.post("api/User/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            });
        }
    };
    var IsEdit='';
    $scope.EditUser = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        IsEdit='Y';
        $scope.SubjectID = id;
        $scope.EditHide = true;
        $http.post("api/User/GetUserDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.UserName = result[0].UserName;
            $scope.FName = result[0].FName;
            $scope.LName = result[0].LName;
            $scope.OrganizationID = result[0].OrganizationID;
            $scope.pwd = result[0].Password;
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
            toastr["error"]('Please Enter User Name', 'Validation');
        }
        else if ($scope.FName=='') {
            toastr["error"]('Please Enter First Name', 'Validation');
        }
        else if ($scope.LName == '') {
            toastr["error"]('Please Enter Last Name', 'Validation');
        }
        else if ($scope.OrganizationID == '') {
            toastr["error"]('Please Select Orgasnization', 'Validation');
        }
        else if ($scope.pwd == '' && IsEdit=='') {
            toastr["error"]('Please Enter Password', 'Validation');
        }
        else if ($scope.Email == '') {
            toastr["error"]('Please Enter Email ID', 'Validation');
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
                UserType:'System'
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
