using System;
using System.Collections.Generic;
using System.Linq;
using UserAuthenticationSystemV2.Models;
using UserAuthenticationSystemV2.Systems;

namespace UserAuthenticationSystemV2.Context
{
    public class ToDoItemContext
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly UniqueGuidGenerationSystem _uniqueGuidGenerationSystem;
        
        public ToDoItemContext(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            _uniqueGuidGenerationSystem = new UniqueGuidGenerationSystem();
        }

        public void Add(Guid userId, ToDoItemViewModel toDo)
        {
            var user = GetUser(userId);

            var newToDoId = _uniqueGuidGenerationSystem.GenerateUniqueId(GetAll(userId).Select(toDo => toDo.Id));

            var newToDo = new ToDoItem(newToDoId, toDo.Title, toDo.Description);
            
            user.ToDoItems.Add(newToDo);

            _applicationDbContext.Users.Update(user);

            _applicationDbContext.SaveChanges();
        }
        
        public void Complite(Guid userId, Guid toDoId)
        {
            var toDo = Get(userId, toDoId);

            toDo.IsComplied = true;

            _applicationDbContext.ToDoItems.Update(toDo);

            _applicationDbContext.SaveChanges();
        }

        public ToDoItem Get(Guid userId, Guid toDoId)
        {
            if (ContainsTask(userId, toDoId) == false)
                throw new NullReferenceException("No ToDo was founded.");

            return GetUser(userId).ToDoItems.First(toDo => toDo.Id == toDoId);
        }
        
        public bool ContainsTask(Guid userId, Guid toDoId)
        {
            return GetAll(userId).Any(toDo => toDo.Id == toDoId);
        }

        public IEnumerable<ToDoItem> GetAll(Guid userId)
        {
            if(ContainsUser(userId) == false)
                throw new NullReferenceException("No user was founded.");
            
            return GetUser(userId).ToDoItems;
        }

        private bool ContainsUser(Guid userId)
        {
            return _applicationDbContext.Users.Any(user => user.Id == userId);
        }
        
        private User GetUser(Guid userId)
        {
            return _applicationDbContext.Users.First(user => user.Id == userId);
        }
    }
}