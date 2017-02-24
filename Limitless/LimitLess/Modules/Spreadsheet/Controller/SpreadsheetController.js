
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
                    console.log(response["data"])
                    if (response["data"]) {

                        toastr["success"]("Saved Successfully.", 'Upload spreadsheet');
                    } else {
                        toastr["error"]('Failed to upload spreadsheet', 'Upload spreadsheet');
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