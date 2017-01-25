/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("TopicController", function ($scope, $http, $rootScope, $location) {
    if ($location.path() == '/topics') {
        $http.get("api/Subject/GetList", {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.Subjects = JSON.parse(data.List);
        });
    }
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'TopicName',
        SortDirection: 'ASC',
    });
    
    if ($location.path() == '/topics') {
        $http.post("api/Topic/GridList", reqdata, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.Records = JSON.parse(data.List);
        });
    }
    
    $scope.clear = function () {
        $scope.TopicID = '';
        $scope.TopicDescription = '';
        $scope.TopicName = '';
        $scope.SubjectID = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.EditTopic = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.TopicID = id;
        $http.post("api/Topic/GetTopicDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.TopicID = result[0].TopicID;
            $scope.TopicDescription = result[0].TopicDescription;
            $scope.TopicName = result[0].TopicName;
            $scope.SubjectID = result[0].SubjectID;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };

    
    $scope.SaveTopics =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            TopicID:$scope.TopicID,
            TopicDescription: $scope.TopicDescription,
            TopicName: $scope.TopicName,
            SubjectID: $scope.SubjectID,
            IsActive: IsActive
        });
        $http({
            method: 'POST',
            url: 'api/Topic/SaveTopics',
            data: Postdata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (res) {
                toastr["success"]("Saved Successfully.", 'Create Subject');
                $http.post("api/Topic/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            },
            function (err) {
                console.log('error...', err);
                toastr["error"](err.error_message, 'Create Subject');
            }
        );
    };
});
