/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("OrganizationController", function ($scope, $http, $rootScope, $location, $timeout) {
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'OrganizationName',
        SortDirection: 'ASC',
    });
    //$.ajax({
    //    type: "POST",
    //    url: "api/Organization/GridList",
    //    data: reqdata,
    //    success: function (data) {
    //        $scope.$apply(function () {
    //            $scope.Records = JSON.parse(data.List);
    //        });
    //    },
    //    error: function (x, y, z) {
    //    }
    //});
    $scope.clear = function () {
        $scope.OrganizationID = '';
        $scope.OrganizationAddress = '';
        $scope.OrganizationName = '';
        $scope.OrganizationPhone = '';
        $scope.OrganizationDescription = '';
        $scope.OrganizationURL = '';
        $('#chkActive').attr('checked', false);
    };
    var id;
    $scope.EditOrganization = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });

        $scope.OrganizationID = id;
        $http.post("api/Organization/GetOrgDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.OrganizationID = result[0].OrganizationID;
            $scope.OrganizationAddress = result[0].OrganizationAddress;
            $scope.OrganizationName = result[0].OrganizationName;
            $scope.OrganizationPhone = result[0].OrganizationPhone;
            $scope.OrganizationDescription = result[0].OrganizationDescription;
            $scope.OrganizationURL = result[0].OrganizationURL;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    $http.post("api/Organization/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });
    $scope.SaveOrganization =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            OrganizationID:$scope.OrganizationID,
            OrganizationAddress: $scope.OrganizationAddress,
            OrganizationName: $scope.OrganizationName,
            OrganizationPhone: $scope.OrganizationPhone,
            OrganizationURL: $scope.OrganizationURL,
            OrganizationDescription: $scope.OrganizationDescription,
            IsActive: IsActive
        });
        $http({
            method: 'POST',
            url: 'api/Organization/SaveOrganization',
            data: Postdata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (res) {
                toastr["success"]("Saved Successfully.", 'Create Organization');
                $http.post("api/Organization/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            },
            function (err) {
                console.log('error...', err);
                toastr["success"](err, 'Create Organization');
            }
        );
    };
});
