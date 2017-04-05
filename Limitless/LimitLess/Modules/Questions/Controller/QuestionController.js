/// <reference path="F:\.NET WorkSpace\Jena's Project\AngularDemoProject\Scripts/Common/app.js" />

app.controller("QuestionController", function ($scope, $http, $location, $cookieStore, $sce) {
    $scope.clear = function () {
        $scope.QuestionID = '';
        $scope.SubObjectiveID = '';
        $scope.QuestionCode = '';
        $scope.QuestionContent = '';
        $scope.Difficulty = '';
        $scope.QuestionTypeId = '';
        $scope.IsActive = '';
        $scope.QuestionImage = '';
        $scope.FinalQuestionContent = '';
        $scope.IsDraggable = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.DeleteQuestion = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        if (confirm('Are you sure you want to delete record?')) {
            $http.post("api/Question/DeleteQuestion", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Deleted Successfully.", 'Delete Question');
                $http.post("api/Question/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            });
        }
    };
    $scope.EditQuestion = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.LessonID = id;
        $http.post("api/Question/GetQuestionDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.QuestionID = result[0].QuestionID;
            $scope.SubObjectiveID = result[0].SubObjectiveID;
            $scope.QuestionCode = result[0].QuestionCode;
            $scope.QuestionContent = result[0].QuestionContent;
            $scope.Difficulty = result[0].Difficulty;
            $scope.QuestionTypeId = result[0].QuestionTypeId;
            $scope.FinalQuestionContent = result[0].FinalQuestionContent;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
            if (result[0].IsDraggable) {
                $('#chkIsDraggable').prop('checked', true);
            }
            else {
                $('#chkIsDraggable').prop('checked', false);
            }
        });
    };

    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'Q.CreatedDate',
        SortDirection: 'DESC',
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    
    $http.post("api/Question/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });
    var ListInput = $.param({
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/SubObjective/GetList", ListInput, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.SubObjectives = JSON.parse(data.List);
    });

    
    $http.get("api/Question/GetQuestionType", {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.QuestionTypes = JSON.parse(data.List);
    });

    var uploadFileToUrl = function (file) {
        var IsActive = $('#chkActive').prop('checked');
        var IsDraggable = $('#chkIsDraggable').prop('checked');
        var fd = new FormData();
        var imageName = $scope.QuestionImage ? $scope.QuestionImage.name : "";
        fd.append('file', file);
        var params = {
            QuestionID: $scope.QuestionID,
            SubObjectiveID: $scope.SubObjectiveID,
            QuestionCode: $scope.QuestionCode,
            QuestionContent: $scope.QuestionContent,
            Difficulty: $scope.Difficulty,
            IsActive: IsActive,
            QuestionTypeId: $scope.QuestionTypeId,
            QuestionImage: imageName,
            FinalQuestionContent: FinalQuestionContent,
            IsDraggable: IsDraggable
        };
        fd.append('parameters', JSON.stringify(params))
        $http.post('api/Question/SaveQuestionWithImage', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined, 'Process-Data': undefined }
        })
        .success(function (res) {
            //alert('Saved Successfully');
            toastr["success"]("Saved Successfully.", 'Create question');
            $http.post("api/Question/GridList", reqdata, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                $scope.Records = JSON.parse(data.List);
            });
        })
        .error(function (err) {
            console.log('error...', err);
            toastr["error"]('Failed to add question', 'Create question');
        });
    }
    $scope.SaveQuestion = function () {
        if ($scope.QuestionImage.name) {
            uploadFileToUrl($scope.QuestionImage);
        }
        else {
            var IsActive = $('#chkActive').prop('checked');
            var IsDraggable = $('#chkIsDraggable').prop('checked');
            var params = $.param({
                QuestionID: $scope.QuestionID,
                SubObjectiveID: $scope.SubObjectiveID,
                QuestionCode: $scope.QuestionCode,
                QuestionContent: $scope.QuestionContent,
                Difficulty: $scope.Difficulty,
                IsActive: IsActive,
                QuestionTypeId: $scope.QuestionTypeId,
                QuestionImage: "",
                FinalQuestionContent: $scope.FinalQuestionContent,
                IsDraggable: IsDraggable
            });
            $http({
                method: 'POST',
                url: 'api/Question/SaveQuestion',
                data: params,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(
                function (res) {
                    //alert('Saved Successfully');
                    toastr["success"]("Saved Successfully.", 'Create question');
                    $http.post("api/Question/GridList", reqdata, {
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                    }).success(function (data) {
                        $scope.Records = JSON.parse(data.List);
                    });
                },
                function (err) {
                    console.log('error...', err);
                    toastr["error"]('Failed to add question', 'Create question');
                });
        }
    };
    $scope.getImagePath = function (imageName) {
        return $sce.trustAsResourceUrl("/images/question_Image/" + imageName);
    }
});