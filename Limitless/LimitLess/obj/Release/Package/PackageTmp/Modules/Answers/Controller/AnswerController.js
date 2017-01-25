/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("AnswerController", function ($scope, $http, $rootScope) {
    $http.get("api/Question/GetList", {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Questions = JSON.parse(data.List);
    });
    $scope.clear = function () {
        $scope.QuestionID = '';
        $scope.AnswerID = '';
        $scope.AnswerCode = '';
        $scope.AnswerContent = '';
        $scope.Explanation = '';
        $scope.IsCorrect = '';
        $scope.IsActive = '';
        $scope.IsCorrect = '';
        $('#chkActive').attr('checked', false);
        $('#chkCorrect').attr('checked', false);
    };
    $scope.EditAnswer = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.LessonID = id;
        $http.post("api/Answer/GetAnswerDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.QuestionID = result[0].QuestionID;
            $scope.AnswerID = result[0].AnswerID;
            $scope.AnswerCode = result[0].AnswerCode;
            $scope.AnswerContent = result[0].AnswerContent;
            $scope.Explanation = result[0].Explanation;
            $scope.IsCorrect = result[0].IsCorrect;
            $scope.IsActive = result[0].IsActive;
            if (result[0].IsCorrect) {
                $('#chkCorrect').prop('checked', true);
            }
            else {
                $('#chkCorrect').prop('checked', false);
            }
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    $scope.SaveAnswer =
   function () {
       var IsActive = $('#chkActive').prop('checked');
       var IsCorrect = $('#chkCorrect').prop('checked');
       var Postdata = $.param({
           QuestionID: $scope.QuestionID,
           AnswerID: $scope.AnswerID,
           AnswerCode: $scope.AnswerCode,
           AnswerContent: $scope.AnswerContent,
           Explanation: $scope.Explanation,
           IsCorrect: IsCorrect,
           IsActive: IsActive
       });
       $http({
           method: 'POST',
           url: 'api/Answer/SaveAnswer',
           data: Postdata,
           headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
       }).then(
           function (res) {
               toastr["success"]("Saved Successfully.", 'Add Answer');
               $http.post("api/Answer/GridList", reqdata, {
                   headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
               }).success(function (data) {
                   $scope.Records = JSON.parse(data.List);
               });
           },
           function (err) {
               console.log('error...', err);
               toastr["error"](err.error_message, 'Add Answer');
           }
       );
   };

    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'AnswerCode',
        SortDirection: 'ASC',
    });
    $http.post("api/Answer/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });
});
