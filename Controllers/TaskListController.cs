using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskListCapstone.Models;


namespace TaskListCapstone.Controllers
{
    public class TaskListController : Controller
    {
        private readonly TaskDbContext _context;

        public TaskListController(Models.TaskDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(_context.AspNetUsers.Where(x => x.Id == id) != null)
            {
                return View(_context.Tasks.Where(tasks => tasks.UserId == id).ToList());
            }
            _context.Tasks.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(Tasks task)
        {
            task.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //TODO Action to update task
        // Send Task "Id" over
        // Find Task by Id
        // Send Tasks Object back to the view to be updated
        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            return View(_context.Tasks.Find(id));
        }

        [HttpPost]
        public IActionResult UpdateTask(Tasks newTasks)
        {
            newTasks.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                Tasks oldTasks = _context.Tasks.Find(newTasks.Id);
                oldTasks.TaskName = newTasks.TaskName;
                oldTasks.Description = newTasks.Description;
                oldTasks.DueDate = newTasks.DueDate;
                oldTasks.Complete = newTasks.Complete;
                oldTasks.Id = newTasks.Id;


                _context.Entry(oldTasks).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Tasks.Update(oldTasks);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            var foundTask = _context.Tasks.Find(id);
            if (foundTask != null)
            {
                _context.Remove(foundTask);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //TODO Action to Mark Task Complete
        // Probably send the task "Id" over
        // Set Complete
        // Set Completetion Status
        
        public IActionResult CompleteTask(int id)
        {
            Tasks oldTasks = _context.Tasks.Find(id);

            oldTasks.Complete = true;
            _context.Entry(oldTasks).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Tasks.Update(oldTasks);
            _context.SaveChanges();

           return RedirectToAction("Index");
        }
    }
}