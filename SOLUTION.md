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


