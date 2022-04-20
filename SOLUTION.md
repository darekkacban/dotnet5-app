TASKS SOLUTION

1. Build - building the app requires 2 things:
- installing dotnet 5 runtime. Maybe it's a good idea to convert the repo to dotnet 6. Then we can can use for example 
global namespaces, implicit usings, and the other new features of C# 10. 
- running a migration
2. This comes down to a sorting problem. I solved this problem Solved by:
- extending TodoListDetailViewmodelFactory. I used LINQ 
OrderBy() extension method. We sort items by enumeration type, which is represented as integer in the memory. So sorting works by default.
- Renamed class: TodoListDetailViewModelFactory to be compliant with Upper camel case format.
- Created a unit test TodoListDetailViewmodelFactoryTest.ShouldSortItemsByImportance(), which tests if the element of High importance is always the first element.
- Unit test pass, so we're safe in case of the future refactorings.
3. Mapping issue cause a test fail.
I found a bug in TodoItemEditFields class constructor, where Importance was always set to Importance.Medium.
It The property should be assigned a constructor parameter, and this is a solution: Importance = importance; Test passed and are green now.
4. Display friendly name instead of a property name.
I solved this problem by adding [DisplayAttribute] in 2 view model classes:
TodoItemEditFields - now the field is called more user friendly in edit fiew
TodoItemCreateFields - I did similar change in creating view. A small bonus to this task:)

The shape of attribute is as follows:
[Display(Name = "Responsible Party")]

5. Checkbox to hide done items. 
I added a checkbox with a label: "Hide done items".

First I add a class in a razor view:
var isDoneClass = @item.IsDone ? "isDone" : "";

And then I add a class at the end of a class list for a row:
<li class="list-group-item @contextualClass @isDoneClass">

In my opinion this solution is not the best, because we end up with multiple spaces in a class attribute of <li> HTML element.
Anyway, the code which I added is compliand with the convention. Thanks to this a code looks like it was written by one person.
If I had more time, I would suggest to improve this approach to eliminate spaces. Maybe combining both classes into one variable,
or even more complex solution. I would avoid hidden fields, and use classes instead like we do now.

Javascript is contained in a global file site.js
$('#hideDoneCheckbox').change(function () {
    if (this.checked) {
        $(".isDone").hide();
    } else {
        $(".isDone").show();
    }
});

We use simple jQuery handlers for a checkbox. All the code is in 1 file. 
For more complex apps we can create 1 javascript file per view.

6. Show also other user lists if at least item is assigned to me.

I extended the predicate inside Where() LINQ method.
Where(tl => tl.Owner.Id == userId || tl.Items.Any(item => item.ResponsiblePartyId == userId));
I tested it for 2 users, and the app shows only required lists.

7. New Rank property
I added the integer Rank property and created a new migration with the command:
dotnet ef migrations add AddRankToTheTodoItem --project=Todo

I applied the changes to the DB with a command:
dotnet ef database update --project=Todo

Sorting on the details page requires a new set of HTML controls:
<li class="list-group-item">
	<input id="orderByRankCheckbox" type="checkbox" name="orderByRankCheckbox">
	<label for="orderByRankCheckbox">Order items by rank</label><br>
</li>

Then javascript part does the work:

Method responsible for sorting (2 options: ascending and descending):

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

And the above method call as a reaction to buttons click:
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

Sorting required 2 new buttons:
<button id="orderByRankAscending">Order by rank ascending</button>
<button id="orderByRankDescending">Order by rank descending</button>
as well as data ttribiute in each data row: data-rank=@item.Rank

8. Gravatar. Ddidtional data like name may be downloaded with public API.
C# model was generated with the website:
https://json2csharp.com/
To see the model, check GravatarContracts folder.

The most important method is located in Gravatar class:

public static string GetUserName(string emailAddress)
{
	var hash = GetHash(emailAddress);
	var url = $"https://en.gravatar.com/";

	try
	{
		var client = new RestClient(url);
		var request = new RestRequest($"{hash}.json");
		var response = client.GetAsync(request).Result;
		Root root = JsonConvert.DeserializeObject<Root>(response.Content);

		return root.entry.First().name.formatted;
	} catch (Exception e)
	{
		return "-"; 
	}
}

All potential errors are handled by returning defailt value "-".
To address the network delays or service unavailability, we could use a library like Polly.

We display Name and surname next to user avatar and email at the top of the website in navigation layout.

9. A new div is created and filled with existing item creation view.
The next step would be modification of the view and save the data with existing controller (or create a new one).
Let's leave it for now.

10. New API endpoint dedicated to Rank update is added. 
We ise it like this from JS:
$('.rankTextbox').change(function () {

    let itemId = $(this).data('item-id');
    let rankValue = $(this).val();

    let url = "/TodoItem/UpdateRank";

    $.post(url, { itemId: itemId, rankValue: rankValue },
        function (response) {
            console.log(response);
        });
});

Also a new bootstrap column was added to show the rank on the list:
<div class="col-md-4">
	<input class="rankTextbox" data-item-id="@item.TodoItemId" type="text" value="@item.Rank"/>
</div>



		

