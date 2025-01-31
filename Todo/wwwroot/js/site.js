﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#hideDoneCheckbox').change(function () {
    if (this.checked) {
        $(".isDone").hide();
    } else {
        $(".isDone").show();
    }
});

function sortTodoItems(descending) {
    return $('.toDoRow').toArray().sort(function (a, b) {
        var rank1 = $(a).data("rank"),
            rank2 = $(b).data("rank");

        if (descending) {
            return rank2 - rank1;
        }

        return rank1 - rank2;
    });
}

$('#orderByRankAscending').click(function () {
    var items = sortTodoItems();
    $('.toDoRow').remove();
    $('.list-group').append(items);
});

$('#orderByRankDescending').click(function () {
    var items = sortTodoItems(true);
    $('.toDoRow').remove();
    $('.list-group').append(items);
});

$('#createNewItem').click(function () {
    let listId = $('#TodoListId').val();
    let url = "/TodoItem/Create?todoListId=" + listId;

    $.get(url).done(function (data) {
        $('#modal').html(data);
    });
});

$('.rankTextbox').change(function () {

    let itemId = $(this).data('item-id');
    let rankValue = $(this).val();

    let url = "/TodoItem/UpdateRank";

    $.post(url, { itemId: itemId, rankValue: rankValue },
        function (response) {
            console.log(response);
        });
});



