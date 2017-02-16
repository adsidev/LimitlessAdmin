
app.controller('SpreadsheetController', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {

    $scope.uploadFiles = function (file, errFiles) {
        console.log("a");
        //upload spreadsheet
        $scope.f = file;
        $scope.errFile = errFiles && errFiles[0];
        if (file) {
            console.log(file);
            file.upload = Upload.upload({
                method: 'POST',
                url: 'http://localhost:60142/api/Spreadsheet/SaveSpreadsheet',
                data: { file: file },
                //headers: { 'Content-Type': 'application/vnd.ms-excel' }
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }
    }
    $scope.uploadImages = function (files, errFiles) {
        console.log("image");
        $scope.files = files;
        $scope.errFiles = errFiles;
        angular.forEach(files, function (file) {
            file.upload = Upload.upload({
                url: 'http://localhost:60142/api/Spreadsheet/SaveImages',
                data: { file: file }
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 *
                                         evt.loaded / evt.total));
            });
        });
    }
}]);