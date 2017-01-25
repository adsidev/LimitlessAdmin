/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("ObjectiveController", function ($scope, $http, $rootScope, $location) {
    
    $scope.clear = function () {
        $scope.TopicID = '';
        $scope.ObjectiveID = '';
        $scope.ObjectiveName = '';
        $scope.ObjectiveDescription = '';
        $('#chkActive').attr('checked', false);
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
    });
    if ($location.path() == '/objectives') {
        $http.post("api/Objective/GridList", reqdata, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.Records = JSON.parse(data.List);
        });
    }
    $http.get("api/Topic/GetList", {
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
