var arrayMap = {};

function showFilterOption(tdObject) {
    var filterGrid = $(tdObject).find(".filter");

    if (filterGrid.is(":visible")) {
        filterGrid.hide();
        return;
    }

    $(".filter").hide();

    var index = 0;
    filterGrid.empty();
    var allSelected = true;
    //filterGrid.append('<div><input id="all" type="checkbox" checked>Tümünü Seç</div>');
    filterGrid.append('<input id="all" type="checkbox" checked style="height:10px; width:15px;"/><span style="padding-top:15px;"> Tümünü Seç </span>');

    var $rows = $(tdObject).parents("table").find("tbody tr");

    $rows.each(function (ind, ele) {
        var currentTd = $(ele).children()[$(tdObject).attr("index")];
        var div = document.createElement("div");
        div.classList.add("grid-item")
        var str = $(ele).is(":visible") ? 'checked' : '';
        if ($(ele).is(":hidden")) {
            allSelected = false;
        }
        //div.innerHTML = '<input type="checkbox" ' + str + ' >' + currentTd.innerHTML;
        div.innerHTML = '<input type="checkbox" ' + str + ' style="height:10px; width:15px;"/><span style="padding-top:15px;"> ' + currentTd.innerHTML + ' </span>';
        filterGrid.append(div);
        arrayMap[index] = ele;
        index++;
    });

    if (!allSelected) {
        filterGrid.find("#all").removeAttr("checked");
    }

    filterGrid.append('<div style="margin-top:5px; margin-bottom:10px;"><input id="close" type="button" value="iptal" style="margin-left:5px; font-size:medium;"/><input id="ok" type="button" value="tamam" style="margin-left:10px; margin-right:5px; font-size:medium;"/></div>');
    filterGrid.show();

    var $closeBtn = filterGrid.find("#close");
    var $okBtn = filterGrid.find("#ok");
    var $checkElems = filterGrid.find("input[type='checkbox']");
    var $gridItems = filterGrid.find(".grid-item");
    var $all = filterGrid.find("#all");

    $closeBtn.click(function () {
        filterGrid.hide();
        return false;
    });

    $okBtn.click(function () {
        filterGrid.find(".grid-item").each(function (ind, ele) {
            if ($(ele).find("input").is(":checked")) {
                $(arrayMap[ind]).show();
            } else {
                $(arrayMap[ind]).hide();
            }
        });
        filterGrid.hide();
        return false;
    });

    $checkElems.click(function (event) {
        event.stopPropagation();
    });

    $gridItems.click(function (event) {
        var chk = $(this).find("input[type='checkbox']");
        $(chk).prop("checked", !$(chk).is(":checked"));
    });

    $all.change(function () {
        var chked = $(this).is(":checked");
        filterGrid.find(".grid-item [type='checkbox']").prop("checked", chked);
    })

    filterGrid.click(function (event) {
        event.stopPropagation();
    });

    return filterGrid;
}