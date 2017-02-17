
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
        console.log("1:"+files);
        $scope.files = files;
        $scope.errFiles = errFiles;
        console.log("2:" + files);
        angular.forEach(files, function (file) {
            console.log("3:" + file);
            file.upload = Upload.upload({
                method: 'POST',
                url: 'http://localhost:60142/api/Spreadsheet/SaveImages',
                data: { file: file }
            });
            console.log("image");
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