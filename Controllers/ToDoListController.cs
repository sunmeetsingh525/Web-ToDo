using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoListMvc.Models;

namespace ToDoListMvc.Controllers
{
    // Controller for managing ToDoList actions.
    public class ToDoListController : Controller
    {
        private readonly ToDoDbContext _context;

        // Constructor injecting the ToDoDbContext via dependency injection.
        public ToDoListController(ToDoDbContext context)
        {
            _context = context;
        }

        // Displays a list of ToDoItems.
        public IActionResult Index()
        {
            var ToDoItems = _context.ToDoItems.ToList();
            return View(_context.ToDoItems);
        }

        // Displays the form for creating a new ToDoItem.
        public IActionResult Create()
        {
            return View();
        }

        // Handles the HTTP POST request to create a new ToDoItem.
        [HttpPost]
        public IActionResult Create(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                // Set a unique Id for the new ToDoItem.
                toDoItem.Id = _context.ToDoItems.Count() + 1;
                _context.ToDoItems.Add(toDoItem);
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

         // Displays the form for editing an existing ToDoItem.
        public IActionResult Edit(int id)
        {
            var toDoItem = _context.ToDoItems.FirstOrDefault(item => item.Id == id);
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
                var existingItem = _context.ToDoItems.FirstOrDefault(item => item.Id == toDoItem.Id);
                if (existingItem != null)
                {
                    // Update the properties of the existing ToDoItem.
                    existingItem.Title = toDoItem.Title;
                    existingItem.Description = toDoItem.Description;
                    existingItem.IsDone = toDoItem.IsDone;
                }
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        // Displays the confirmation page for deleting a ToDoItem.
        public IActionResult Delete(int id)
        {
            var toDoItem = _context.ToDoItems.FirstOrDefault(item => item.Id == id);
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
            // Find the ToDoItem to be deleted.
            var toDoItem = _context.ToDoItems.FirstOrDefault(item => item.Id == id);
            if (toDoItem != null)
            {
                _context.ToDoItems.Remove(toDoItem);
            }
            return RedirectToAction("Index");
        }
    }
}