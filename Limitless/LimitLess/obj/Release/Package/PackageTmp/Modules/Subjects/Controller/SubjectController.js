/// <reference path="F:\PractiveLession\MVCApplications\AngularDemoProject\AngularDemoProject\Scripts/Common/app.js" />

app.controller("SubjectController", function ($scope, $http, $rootScope, $location, $cookieStore) {
    var reqdata = $.param({
        PageIndex: 1,
        PageSize: 10,
        OrderBy: 'SubjectName',
        SortDirection: 'ASC',
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/Subject/GridList", reqdata, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Records = JSON.parse(data.List);
    });
   
    var ListInput = $.param({
        OrganizationID: $cookieStore.get('OrganizationID')
    });
    $http.post("api/Organization/GetList",ListInput, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
    }).success(function (data) {
        $scope.Organizations = JSON.parse(data.List);
    });
    
    $scope.clear = function () {
        $scope.SubjectID = '';
        $scope.SubjectName = '';
        $scope.SubjectDescription = '';
        $scope.SubjectICON = '';
        $scope.OrganizationID = '';
        $('#chkActive').attr('checked', false);
    };
    $scope.DeleteSubject = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        if (confirm('Are you sure you want to delete record?'))
        {
            $http.post("api/Subject/DeleteSubject", req_data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                toastr["success"]("Deleted Successfully.", 'Delete Subject');
                $http.post("api/Subject/GridList", reqdata, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
                }).success(function (data) {
                    $scope.Records = JSON.parse(data.List);
                });
            });
        }
    };
    $scope.EditSubject = function (e) {
        id = $(e.target).data('id');
        var req_data = $.param({
            ID: id
        });
        $scope.SubjectID = id;
        $http.post("api/Subject/GetSubjectDetails", req_data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            var result = JSON.parse(data.SelectedDetails);
            $scope.SubjectID = result[0].SubjectID;
            $scope.SubjectName = result[0].SubjectName;
            $scope.SubjectDescription = result[0].SubjectDescription;
            $scope.SubjectICON = result[0].SubjectICON;
            $scope.OrganizationID = result[0].OrganizationID;
            if (result[0].IsActive) {
                $('#chkActive').prop('checked', true);
            }
            else {
                $('#chkActive').prop('checked', false);
            }
        });
    };
    $scope.SaveSubject =
    function () {
        var IsActive = $('#chkActive').prop('checked');
        var Postdata = $.param({
            SubjectName: $scope.SubjectName,
            SubjectDescription: $scope.SubjectDescription,
            SubjectICON: $scope.SubjectICON,
            OrganizationID: $scope.OrganizationID,
            IsActive: IsActive,
        });
        $http.post("api/Subject/SaveSubject", Postdata, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).success(function (data) {
            toastr["success"]("Saved Successfully.", 'Create Subject');
            $http.post("api/Subject/GridList", reqdata, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(function (data) {
                $scope.Records = JSON.parse(data.List);
            });
            //$location.path('/subjects');
        }).error(function (error) {
            toastr["error"](error.error_message, 'Create Subject');
        });
    };
});
