#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concert.Data;
using Concert.Data.Entities;
using Concert.Models;

namespace Concert.Controllers
{
    public class Tickets1Controller : Controller
    {
        private readonly DataContext _context;

        public Tickets1Controller(DataContext context)
        {
            _context = context;
        }

        // GET: Tickets1
        public async Task<IActionResult> Index()
        {
            return View(await _context.tickets.ToListAsync());
        }

        // GET: Tickets1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WasUsed,Document,Name,Date")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.tickets.FindAsync(id);

            TicketViewModel model = new()
            {
                Id = ticket.Id,
                Document = ticket.Document,
                Name = ticket.Name,
                Date = DateTime.Now,
                EntranceId = 0,
                Entrances = (IEnumerable<SelectListItem>)_context.entrances.FirstOrDefault(),

            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Tickets1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel model)
        {
            Entrance entrance = new() { tickets = new List<Ticket>() };
            entrance = await _context.entrances.FindAsync(1);

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new()
                    {
                        Id = model.Id,
                        WasUsed = true,
                        Document = model.Document,
                        Name = model.Name,
                        Entrance = await _context.entrances.FindAsync(model.EntranceId)
                    };

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Consult));
            }
            return RedirectToAction(nameof(Consult));
            //Entrance entrance = new() { tickets = new List<Ticket>() };
            //entrance = await _context.entrances.FindAsync(1);

            //if (id != ticket.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        ticket.Entrance.Id = entrance.Id;
            //        _context.tickets.Include(t => t.Entrance == entrance);
            //        _context.Update(ticket);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TicketExists(ticket.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Consult));
            //}
            //return RedirectToAction(nameof(Consult));
        }

        // GET: Tickets1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            _context.tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.tickets.Any(e => e.Id == id);
        }
        public IActionResult Consult()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Consult(int id)
        {
            if (ModelState.IsValid)
            {
                Ticket tiket = await _context.tickets.FindAsync(id);

                if (id < 1 || id > 5000)
                {
                    ModelState.AddModelError(string.Empty, "Ese número de boleta no existe; Ingrese uno existente");
                    return View(tiket);
                }

                if (id >= 1 || id <= 5000)
                {
                    if (tiket.WasUsed == true)
                    {
                        return RedirectToAction(nameof(Details), tiket);
                    }
                    if (tiket.WasUsed == false)
                    {
                        return RedirectToAction(nameof(Edit) , tiket);
                    }
                }
            }
            return View(await _context.tickets.FindAsync(id));
        }

    }
}
