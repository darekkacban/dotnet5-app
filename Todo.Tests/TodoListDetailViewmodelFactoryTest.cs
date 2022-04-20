using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Xunit;

namespace Todo.Tests
{
    public class TodoListDetailViewmodelFactoryTest
    {
        [Fact]
        public void ShouldSortItemsByImportance()
        {
            var user = new IdentityUser() { Email = "a@a.com", UserName = "Darek" };

            var list = new TodoList(user, "list 1")
            {
                Items = new List<TodoItem>()
                {
                    new TodoItem(1, "1", "item 1", Importance.Medium, 1)
                    {
                        ResponsibleParty = user
                    },
                    new TodoItem(2, "1", "item 2", Importance.Low, 1)
                    {
                        ResponsibleParty = user
                    },
                    new TodoItem(3, "1", "item 3", Importance.High, 1)
                    {
                        ResponsibleParty = user
                    }
                }
            };

            var viewModelList = TodoListDetailViewModelFactory.Create(list);

            Assert.Equal(Importance.High, viewModelList.Items.First().Importance);
        }
    }
}
