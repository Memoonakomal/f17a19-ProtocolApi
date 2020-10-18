using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProtocolApi.Models;

namespace ProtocolApi.Controllers
{
    public class ProtocolsController : Controller
    {
        private readonly ProtocolContext _context;

        public ProtocolsController(ProtocolContext context)
        {
            _context = context;
        }

        // GET: Protocols
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItems.ToListAsync());
        }

        // GET: Protocols/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocol = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (protocol == null)
            {
                return NotFound();
            }

            return View(protocol);
        }

        // GET: Protocols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Protocols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsComplete")] Protocol protocol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(protocol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(protocol);
        }

        // GET: Protocols/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocol = await _context.TodoItems.FindAsync(id);
            if (protocol == null)
            {
                return NotFound();
            }
            return View(protocol);
        }

        // POST: Protocols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsComplete")] Protocol protocol)
        {
            if (id != protocol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(protocol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtocolExists(protocol.Id))
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
            return View(protocol);
        }

        // GET: Protocols/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocol = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (protocol == null)
            {
                return NotFound();
            }

            return View(protocol);
        }

        // POST: Protocols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var protocol = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(protocol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtocolExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
