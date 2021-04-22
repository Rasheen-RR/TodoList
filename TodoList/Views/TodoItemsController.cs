using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Context;
using TodoList.Models;

namespace TodoList.Views
{
    public class TodoItemsController : Controller
    {
        private readonly RepositoryContext _context;

        public TodoItemsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: TodoItems
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            return View(await _context.TodoItem.Where(m => m.UserEmail == userEmail).ToListAsync());
        }

        // GET: TodoItems/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        [Authorize]

        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("Title,Description,DueDate,Done")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.UserEmail = User.Identity.Name;
                todoItem.Addeddate = DateTime.Now;
                todoItem.DoneDate = null;
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("Id,UserEmail,Title,Description,Addeddate,DueDate,Done,DoneDate")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (todoItem.Done)
                    {
                        todoItem.DoneDate = DateTime.Now;
                    }
                    else
                    {
                        todoItem.DoneDate = null;
                    }

                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }


        [HttpPost, ActionName("Change")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Apply(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                TodoItem todoItem = new TodoItem();
                todoItem.Id = Int32.Parse(form["Id"]);
                todoItem.UserEmail = form["UserEmail"].ToString();
                todoItem.Title = form["Title"].ToString();
                todoItem.Description = form["Description"].ToString();
                todoItem.Done = bool.Parse(form["Done"].ToString());
                todoItem.DueDate = DateTime.Parse(form["DueDate"].ToString());
                todoItem.Addeddate = DateTime.Parse(form["Addeddate"].ToString());

                if (todoItem.Done)
                {
                    todoItem.Done = false;
                    todoItem.DoneDate = null;
                }
                else
                {
                    todoItem.Done = true;
                    todoItem.DoneDate = DateTime.Now;
                }




                _context.Update(todoItem);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
