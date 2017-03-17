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
            });

            file.upload.then(
                function (response) {
                    console.log(response);
                    if (response["data"][0] != "0") {                                   //inserted 
                        toastr["success"](response["data"][1], "Saved Successfully.");
                        if (response["data"][0] == "2") {                               // part inserted
                            //automatically download txt, the txt file include the response[data] except for response[data][0]
                            SaveAsFile(response["data"], "result_log.txt", "text/plain;charset=utf-8");
                        }
                    } else {
                        toastr["error"](response["data"][1], "Saved Failure.");
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

        function SaveAsFile(t, f, m) {
            try {
                var b = new Blob([t], { type: m });
                saveAs(b, f);
            } catch (e) {
                window.open("data:" + m + "," + encodeURIComponent(t), '_blank', '');
            }
        }
    }


}]);