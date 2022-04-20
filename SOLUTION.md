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