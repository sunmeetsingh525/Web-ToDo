using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoListMvc.Models;

namespace ToDoListMvc.Controllers
{
    public class ToDoListController : Controller
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        public IActionResult Index()
        {
            return View(_toDoItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                toDoItem.Id = _toDoItems.Count + 1;
                _toDoItems.Add(toDoItem);
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        public IActionResult Edit(int id)
        {
            var toDoItem = _toDoItems.FirstOrDefault(item => item.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        [HttpPost]
        public IActionResult Edit(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                var existingItem = _toDoItems.FirstOrDefault(item => item.Id == toDoItem.Id);
                if (existingItem != null)
                {
                    existingItem.Title = toDoItem.Title;
                    existingItem.Description = toDoItem.Description;
                    existingItem.IsDone = toDoItem.IsDone;
                }
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        public IActionResult Delete(int id)
        {
            var toDoItem = _toDoItems.FirstOrDefault(item => item.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var toDoItem = _toDoItems.FirstOrDefault(item => item.Id == id);
            if (toDoItem != null)
            {
                _toDoItems.Remove(toDoItem);
            }
            return RedirectToAction("Index");
        }
    }
}