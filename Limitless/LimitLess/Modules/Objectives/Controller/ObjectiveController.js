/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("ObjectiveController", function ($scope, $http, $rootScope, $location, $cookieStore) {
    
    $scope.clear = function () {
        $scope.TopicID = '';
        $scope.ObjectiveID = '';
        $scope.ObjectiveName = '';
        $scope.ObjectiveDescription = '';
        $scope.ObjectiveCode = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.DeleteObjective = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        if (confirm('Are you sure you want to delete record?')) {
            $http.post("api/Objective/DeleteObjective", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Deleted Successfully.", 'Delete Objective');
                $http.post("api/Objective/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            });
        }
    };
    $scope.EditObjective = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.ObjectiveID = id;
        $http.post("api/Objective/GetObjectiveDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.TopicID = result[0].TopicID;
            $scope.ObjectiveName = result[0].ObjectiveName;
            $scope.ObjectiveDescription = result[0].ObjectiveDescription;
            $scope.ObjectiveID = result[0].ObjectiveID; 
            $scope.ObjectiveCode = result[0].ObjectiveCode;
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
        OrderBy: 'ObjectiveName',
        SortDirection: 'ASC',
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    if ($location.path() == '/objectives') {
        $http.post("api/Objective/GridList", reqdata, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.Records = JSON.parse(data.List);
        });
    }
    var ListInput = $.param({
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/Topic/GetList", ListInput, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Topics = JSON.parse(data.List);
    });

    $scope.SaveObjectives =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            ObjectiveID:$scope.ObjectiveID,
            TopicID: $scope.TopicID,
            ObjectiveName: $scope.ObjectiveName,
            ObjectiveDescription: $scope.ObjectiveDescription,
            IsActive: IsActive,
            ObjectiveCode: $scope.ObjectiveCode,
        });
        $http({
            method: 'POST',
            url: 'api/Objective/SaveObjective',
            data: Postdata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (res) {
                //alert('Saved Successfully');
                toastr["success"]("Saved Successfully.", 'Create Objective');
                $http.post("api/Objective/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            },
            function (err) {
                console.log('error...', err);
                toastr["success"](err.error_message, 'Create Objective');
            }
        );
    };
});
