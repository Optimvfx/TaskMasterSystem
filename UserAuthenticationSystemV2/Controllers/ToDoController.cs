using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationSystemV2.Context;
using UserAuthenticationSystemV2.Controllers.Base;
using UserAuthenticationSystemV2.Generators.PasswordEncryptor;
using UserAuthenticationSystemV2.Models;
using UserAuthenticationSystemV2.Systems;

namespace UserAuthenticationSystemV2.Controllers
{
    public class ToDoController : CookieReadController
    {
        private readonly ToDoItemContext _toDoItemContext;

        public ToDoController(ToDoItemContext toDoItemContext)
        {
            _toDoItemContext = toDoItemContext;
        }

        [HttpPost]
        public  async Task<IActionResult> CreateToDo(ToDoItemViewModel toDoItem)
        {
            if (TryGetUserLoginFormByCookie(out LoginViewModel userModel) == false)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError("", "Некорректные данные");
                
                return View(toDoItem);
            }

            _toDoItemContext.Add();
        }
    }
}