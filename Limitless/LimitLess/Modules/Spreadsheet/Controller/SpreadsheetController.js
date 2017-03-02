
app.controller('SpreadsheetController', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {

    $scope.uploadFiles = function (file, errFiles) {
        //upload spreadsheet
        $scope.f = file;
        $scope.errFile = errFiles && errFiles[0];
        if (file) {
            file.upload = Upload.upload({
                method: 'POST',
                url: 'http://localhost:60142/api/Spreadsheet/SaveSpreadsheet',
                data: { file: file },
                //headers: { 'Content-Type': 'application/vnd.ms-excel' }
            });

            file.upload.then(
                function (response) {
                    console.log(response);
                    if (response["data"] != "excel format is not correct") {
                        console.log("correct"+response["data"])
                        toastr["success"](response["data"], "Saved Successfully.");
                    } else {
                        console.log("error"+ response["data"])
                        toastr["error"](response["data"], "Saved Failure.");
                    }
                    $timeout(function () {
                        file.result = response.data;
                    });

                }, function (err) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                });
        }
    }

}]);