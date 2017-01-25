/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("LessonController", function ($scope, $http, $rootScope, $location) {
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'LessonContent',
        SortDirection: 'ASC',
    });
    $scope.clear = function () {
        $scope.LessonID = '';
        $scope.LessonContent = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.EditLesson = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.LessonID = id;
        $http.post("api/Lesson/GetLessonDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.LessonID = result[0].LessonID;
            $scope.LessonContent = result[0].LessonContent;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    if ($location.path() == '/lessons') {
        $http.post("api/Lesson/GridList", reqdata, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            $scope.Records = JSON.parse(data.List);
        });
    }
    $scope.SaveLesson =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            LessonID: $scope.LessonID,
            LessonContent: $scope.LessonContent,
            IsActive: IsActive
        });
        $http({
            method: 'POST',
            url: 'api/Lesson/SaveLesson',
            data: Postdata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(
            function (res) {
                toastr["success"]("Saved Successfully.", 'Create Lesson');
                $http.post("api/Lesson/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            },
            function (err) {
                console.log('error...', err);
                toastr["success"](err.error_message, 'Create Lesson');
            }
        );
    };
});
