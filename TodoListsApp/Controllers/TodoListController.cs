
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TodoListsApp.Data;
using TodoListsApp.Models;

namespace TodoListsApp.Controllers
{
    public class TodoListController : Controller
    {
        private readonly AppDbContext _db;
        
        public TodoListController(AppDbContext db)
        {
            _db = db;
            
        }

        

        public IActionResult Index()
        {
            IEnumerable<TodoList> todoList = _db.TodoLists;
            return View(todoList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoList todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.TodoLists.Add(todo);
                    _db.SaveChanges();
                    TempData["success"] = "Todo List added successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try Again!. if the problem persists, see your system administrator.");
            }
            return View(todo);
        }

        //GET
        public IActionResult Edit(string id)
        {

            string decryptId = Encryption.decrypt(id);
            if (id == null || id == "")
            {
                return NotFound();
            }

            id = decryptId;
            int newId = Convert.ToInt32(id);
            var TodoListFromDb = _db.TodoLists.Where(x => x.Id == newId).Select(x => new TodoListViewModel()
            {
               Id = x.Id,
               ListContent=x.ListContent,
               Time=x.Time,
               Priority=x.Priority

            }).FirstOrDefault();
            if (TodoListFromDb == null)
            {
                return NotFound();
            }

            return View("Edit",TodoListFromDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TodoListViewModel todo, string id)
        {
            try
            {
               
                    string decryptId = Encryption.decrypt(id);
                    if (id == null || id == "")
                    {
                        return NotFound();
                    }

                    id = decryptId;
                    int newId = Convert.ToInt32(id);

                    var getId = _db.TodoLists.Where(x => x.Id == newId).FirstOrDefault();
                    getId.ListContent = todo.ListContent;
                    getId.Time = todo.Time;
                    getId.Priority = todo.Priority;
                   _db.SaveChanges();
                    TempData["success"] = "Todo List updated successfully!";
                    return RedirectToAction("Index");
                
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try Again!. if the problem persists, see your system administrator.");
            }
            return View(todo);
        }

        //GET
        public IActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again!. if the error persists, see your system administrator";
            }
            var TodoListFromDb = _db.TodoLists.Find(id);
            if (TodoListFromDb == null)
            {
                return NotFound();
            }
            return View(TodoListFromDb);
        }

        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            try
            {
                var todo = _db.TodoLists.Find(id);
                if (todo == null)
                {
                    return NotFound();
                }

                _db.TodoLists.Remove(todo);
                _db.SaveChanges();
                TempData["success"] = "Todo list deleted successfully!";
                return RedirectToAction("Index");
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new {id=id, saveChangesError=true});
            }

        }
    }
}
