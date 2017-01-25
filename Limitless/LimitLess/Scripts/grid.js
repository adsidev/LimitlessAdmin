(function ($) {
    var pagingNeeded = false;
    var gridGenerateBody = "";
    var $table;
    var opts;
    $.fn.gridcolumn = function (options) {
        var thisSelector = this.selector;
        var opt = $.extend({}, $.fn.gridcolumn.defaults, options);
        return opt;
    };
    $.fn.gridcolumn.defaults = {
        heading: "Heading",
        data: "json_field",
        type: "string",
        sortable: true,
        starthidden: false,
        orderby: "",
        sortDirection: ""
    };
    $.fn.grid = function (options) {
        var thisSelector = this.selector;

        opts = $.extend({}, $.fn.grid.defaults, options);

        var $gridContainer = $("<div>", { "data-so": "A", "data-ps": opts.pageSize }).addClass("mrjt");

        var $headerStrip = $("<div>").addClass("row gridMenuOptions nopad nomar padding-bottom");
        var $numofRecords = $("<select>").addClass("col-sm-3 selectDrop").append($("<option>", { value: 1, text: "10" }));
        $numofRecords.append($("<option>", { value: 2, text: "20" }));
        $numofRecords.append($("<option>", { value: 3, text: "50" }));
        $numofRecords.append($("<option>", { value: 4, text: "100" }));

        var $columns = $("<select>").addClass("col-sm-3 selectDrop margin-left").append($("<option>", { value: 1, text: "Select Column" }));

        var $txtBox = $("<input>").addClass("input").css("padding", "2px");
        var $searchBtnGrid = $("<input>").addClass("btn btn-theme margin-left btnWidth").attr({ type: "button", id: "field", value: "Search" });
        var $visibleColumnsCbList = $("<ul>").addClass("legend");

        $table = $("<table>").addClass(opts.tableClass).addClass("table grid tableActions");
        var $thead = $("<thead>");
        var $theadRow = $("<tr>");
        $.each(opts.columns, function (index, item) {
            var $th = $("<th>").attr("data-i", index).addClass(item.cssClass);

            if (item.heading !== "" && item.heading.toLowerCase() !== "delete")
                $columns.append($("<option>", { value: index, text: item.heading }));
            var $cb = $("<input>", { "type": "checkbox", "id": "cb" + thisSelector + index, value: index, checked: !item.starthidden, "data-i": index }).bind("change", opts.onHiddenCBChange); //.appendTo($visibleColumnsCbList);
            var $cblabel = $("<li />", { 'for': "jcb" + thisSelector + index, text: item.heading });
            $cb.prependTo($cblabel);
            $cblabel.appendTo($visibleColumnsCbList);
            if (item.starthidden) {
                $th.hide();
            }
            if (item.sortable) {
                $("<a>", { "class": "s-init", "href": "#", "data-i": index, "data-t": item.type, "data-field": item.data }).text(item.heading).bind("click", opts.onSortClick).appendTo($th);
            } else {
                $("<span>").text(item.heading).appendTo($th);
            }
            $th.appendTo($theadRow);
        });
        //$($numofRecords).idealselect();

        //$($columns).idealselect();
        ////$("<a>", { "class": "s-init", "href": "#" }).text("ssd").bind("click", opts.onSortClick).appendTo($btnSearch);
        //var $columnContainer = $("<div>");
        //$columnContainer.append("<div class=\"pull-right gridSearchField margin-right\" style=\"width:250px\"><input type=\"text\" class=\"search-input form-control\" style=\"width:200px\" placeholder=\"Search...\" id=\"search\"><span class=\"search-icon btngridSearch\"> <i class=\"fa icon-search3\"></i></span>");
        //$("<a>", { "class": "s-init", "href": "#"}).text("sss").bind("click", opts.onSortClick).appendTo($columnContainer);
        //$columnContainer.append("</div><div class=\"gridHideViewOptions\"><a class=\"menu-icon toggleDropMenu btn btn-black\"><i class=\"fa icon-menu\"></i></a><div class=\"addRemoveColumn dropMenu\">" +
        //"<div class=\"DropdownTopIcon\">" +
        //" <span class=\"pull-left\"><label><input type=\"checkbox\" id=\"settingchkall\" value=\"lastName\">Select All</label></span>" +
        //"<i class=\"fa icon-menu\"></i>" +
        //"</div>" +
        //"<div class=\"gridDropSearch\"><button class=\"search_btn\"><i class=\"icon-search5\"></i></button><input type=\"text\" placeholder=\"Search ...\"></div>" +
        //"<ul class=\"custom-scroll\">" +
        //"<div>" +
        //"<li class=\"checkbox\">" +
        //"<label>" +
        //"<input type=\"checkbox\" value=\"firstName\">" +
        //" First Name" +
        //"</label>" +
        //"</li>" +

        //"</div>" +
        //"</ul>" +
        //"<div class=\"col-sm-12 nopad text-center text-left-xs  padding-bottom padding-top\">" +
        //"<button class=\"btn btn-theme \">Cancel</button>" +
        //"<button class=\"btn btn-theme\" id=\"add-btn\">Save</button>" +
        //    "</div>" +
        //    "</div>" +
        //    "</div>");
        //$headerStrip.append($columnContainer);
        $gridContainer.append($headerStrip);

        //$gridContainer.append($visibleColumnsCbList);
        $theadRow.appendTo($thead);
        $thead.appendTo($table);
        gridGenerateBody(opts, $table, 1);
        $gridContainer.append($table);
        //if (pagingNeeded) {
        var $pager = $("<ul>").addClass("pagination");
        //for (var i = 0; i < Math.ceil(opts.data.length / opts.pageSize) ; i++) {
        //    var $li = $("<li>");
        //    $("<a>", { "text": "" + (i + 1), "href": "#", "data-i": (i + 1), "class": "p-link" }).bind("click", opts.onPageClick).appendTo($li);
        //    $pager.append($li);
        //}
        //$gridContainer.append($pager).addClass("paged");

        //var $pager = $("<ul>").addClass("pagination");
        //for (var i = 0; i < Math.ceil(opts.data.length / opts.pageSize) ; i++) {
        //    $("<a>", { "text": "Page " + (i + 1), "href": "#", "data-i": (i + 1), "class": "p-link" }).bind("click", opts.onPageClick).appendTo($pager);
        //}
        var $bottomStrip = $("<div>").addClass("col-sm-12 nopad gridfooterdiv tableFooterNav panel-footer");
        $("<div style=\"margin-top:5px\">").addClass("col-sm-2").append("<select style=\"width:70px\" class=\"form-control\">" +
                                            "<option selected=\"\" value=\"10\">10</option>" +
                                            "<option value=\"20\">20</option>" +
                                            "<option value=\"50\">50</option>" +
                                            "<option value=\"100\">100</option>" +
                                            "</select>").appendTo($bottomStrip);
        $("<div>").addClass("col-sm-2 nopad gridnumofRecords").append("1-10 showing in 100 Records.").appendTo($bottomStrip);
        $("<div>").addClass("col-sm-8 nopad").append(($pager).addClass("paged")).appendTo($bottomStrip);
        $gridContainer.append($bottomStrip);
        //$($pager).pagination({
        //    pages: Math.ceil(opts.totalRecords / opts.pageSize),
        //    cssStyle: "light-theme",//'dark-theme',
        //    displayedPages: 5,
        //    edges: 3,
        //    onPageClick: function (pageNumber) {
        //        gridGenerateBody(opts, $table, pageNumber);
        //    }
        //});
        //}
        return this.append($gridContainer);
    };
    gridGenerateBody = function (opts, table, pageNumber) {
        var pagingData = {
            PageIndex: pageNumber,
            PageSize: opts.pageSize,
            OrderBy: opts.orderBy,
            SortDirection: opts.sortDirection
        };
        $.ajax({
            url: opts.url,
            async: false,
            type: "POST",
            dataType: "json",
            data: pagingData,
            headers: {
                'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
            },
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, jqXhr) {
                opts.data = JSON.parse(data.List);
                opts.totalRecords = data.TotalRecords;
                $(table).find("tbody").html("");
                $.each(opts.data, function (index, item) {
                    var $tr = $("<tr>").attr("data-i", index);

                    if (opts.pageSize <= index) {
                        $tr.hide();
                        pagingNeeded = true;
                    }
                    $.each(opts.columns, function (cIndex, cItem) {

                        var $td = "";
                        if (cItem.template !== "" && typeof cItem.template !== "undefined") {
                            var text = cItem.template;
                            cItem.template.replace(/{{(.*?)}}/g, function (g0, g1) {
                                text = cItem.template.replace(g0, item[cItem.data]);
                            });

                            ////cItem.template = cItem.template.replace("");
                            //debugger
                            $td = $("<td>").html(text);
                            //console.log(matches);
                            //$td = $("<td>").text(item[cItem.data]).attr("data-i", cIndex);
                        } else {
                            $td = $("<td>").text(item[cItem.data]).attr("data-i", cIndex);
                        }
                        if (cItem.starthidden) {
                            $td.hide();
                        }
                        $td.appendTo($tr);
                    });
                    $tr.appendTo(table);
                });
            }
        });
    };
    $.fn.grid.defaults = {
        cssClass: "table",
        columns: [],
        data: [],
        pageSize: 10,

        onHiddenCBChange: function () {
            var $thisGrid = $(this).parents(".mrjt");
            var columIndex = $(this).attr("data-i");

            if ($(this).is(":checked")) {
                $("td[data-i='" + columIndex + "']", $thisGrid).show();
                $("th[data-i='" + columIndex + "']", $thisGrid).show();
            } else {
                $("td[data-i='" + columIndex + "']", $thisGrid).hide();
                $("th[data-i='" + columIndex + "']", $thisGrid).hide();
            }
        },
        onPageClick: function () {
            var $thisGrid = $(this).parents(".mrjt");

            var pageSize = $thisGrid.attr("data-ps");
            var page = $(this).attr("data-i");

            $("tbody tr", $thisGrid).each(function (trIndex, trItem) {
                $(this).hide();

                var pageStart = ((page - 1) * pageSize) + 1;
                var pageEnd = page * pageSize;

                if ((trIndex + 1) >= pageStart && (trIndex + 1) <= pageEnd) {
                    $(this).show();
                }
            });

            return false;
        },
        onSortClick: function () {
            opts.orderBy = $(this).attr("data-field");
            if (typeof opts.sortDirection === "undefined" || opts.sortDirection === null) {
                opts.sortDirection = "DESC";
            } else if (opts.sortDirection === "ASC") {
                opts.sortDirection = "DESC";
            }
            else if (opts.sortDirection === "DESC") {
                opts.sortDirection = "ASC";
            }
            gridGenerateBody(opts, $table, 1);
            //alert(7);
            //var $thisGrid = $(this).parents(".mrjt");
            //var direction = $thisGrid.attr("data-so");

            //$(".s-init", $thisGrid).removeClass("s-A s-D");
            //$(this).addClass("s-" + direction);

            //var type = $(this).attr("data-t");
            //var index = $(this).attr("data-i");

            //var array = [];

            //$("tbody tr", $thisGrid).each(function (trIndex, trItem) {
            //    var item = $("td", trItem).eq(index);
            //    var trId = item.parent().attr("data-i");

            //    var value = null;
            //    switch (type) {
            //        case "string":
            //            value = item.text();
            //            break;
            //        case "int":
            //            value = parseInt(item.text());
            //            break;

            //        case "float":
            //            value = parseFloat(item.text());
            //            break;

            //        case "datetime":
            //            value = new Date(item.text());
            //            break;

            //        default:
            //            value = item.text();
            //            break;
            //    }

            //    array.push({ tr_id: trId, val: value });
            //});

            //if (direction === "A") {
            //    array.sort(function (a, b) {
            //        if (a.val > b.val) { return 1 }
            //        if (a.val < b.val) { return -1 }
            //        return 0;
            //    });
            //    $thisGrid.attr("data-so", "D");
            //} else {

            //    array.sort(function (a, b) {
            //        if (a.val < b.val) { return 1 }
            //        if (a.val > b.val) { return -1 }
            //        return 0;
            //    });

            //    $thisGrid.attr("data-so", "A");
            //}

            //for (var i = 0; i < array.length; i++) {
            //    var td = $("tr[data-i='" + array[i].tr_id + "']", $thisGrid);
            //    td.detach();

            //    $("tbody", $thisGrid).append(td);
            //}

            //if ($thisGrid.hasClass("paged")) {
            //    $(".p-link", $thisGrid).eq(0).click();
            //}

            return false;
        }
    };

}(jQuery));
angular.element(document).ready(function () {
    $("body").undelegate().delegate(".btntblClmnsDropMenu", "click", function () {
        $(this).next().toggle();
    });
    $("body").undelegate().delegate(".btngridSearch", "click", function () {
        alert(2);
    });
});
