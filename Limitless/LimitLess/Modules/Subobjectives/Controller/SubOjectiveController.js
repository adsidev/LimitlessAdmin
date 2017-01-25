/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("SubObjectiveController", function ($scope, $http, $rootScope, $location, $cookieStore) {
    $scope.clear = function () {
        $scope.ObjectiveID = '';
        $scope.SubObjectivesID = '';
        $scope.SubObjectiveName = '';
        $scope.SubObjectiveDescription = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.DeleteSubObjective = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        if (confirm('Are you sure you want to delete record?')) {
            $http.post("api/SubObjective/DeleteSubObjective", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Deleted Successfully.", 'Delete Sub Objective');
                $http.post("api/SubObjective/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            });
        }
    };
    $scope.EditSubObjective = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.SubObjectivesID = id;
        $http.post("api/SubObjective/GetSubObjectiveDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.SubObjectivesID = result[0].SubObjectivesID;
            $scope.ObjectiveID = result[0].ObjectivesID;
            $scope.SubObjectiveName = result[0].SubObjectiveName;
            $scope.SubObjectiveDescription = result[0].SubObjectiveDescription;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'SubObjectiveName',
        SortDirection: 'ASC',
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/SubObjective/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });

    var ListInput = $.param({
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/Objective/GetList", ListInput, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Objectives = JSON.parse(data.List);
    });

    $scope.SaveSubObjectives =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            SubObjectivesID: $scope.SubObjectivesID,
            ObjectivesID: $scope.ObjectiveID,
            SubObjectiveName: $scope.SubObjectiveName,
            SubObjectiveDescription: $scope.SubObjectiveDescription,
            IsActive: IsActive
        });
        $http({
            method: 'POST',
            url: 'api/SubObjective/SaveSubObjective',
            data: Postdata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (res) {
                toastr["success"]("Saved Successfully.", 'Create Sub Objective');
                $http.post("api/SubObjective/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            },
            function (err) {
                console.log('error...', err);
                toastr["success"](err.error_message, 'Create Sub Objective');
            }
        );
    };
});
