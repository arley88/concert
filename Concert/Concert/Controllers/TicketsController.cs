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
using Concert.Helpers;

namespace Concert.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
       //private readonly ICombosHelper _combosHelper;

        public TicketsController(DataContext context)
        {
            _context = context;
            //_combosHelper = combosHelper;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.tickets.ToListAsync());
        }

        
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            if (ModelState.IsValid)
            {
                Ticket tiket = await _context.tickets.FindAsync(id);

                if (id < 1 || id > 50000)
                {
                    ModelState.AddModelError(string.Empty, "Ese número de boleta no existe");
                    return RedirectToAction(nameof(Create));
                }

                if (id >= 1 || id <= 50000)
                {
                    if (tiket.WasUsed == true)
                    {
                        
                        await _context.tickets
                            .Include(s => s.Entrance)
                            .FirstOrDefaultAsync(m => m.Id == id);
                        if (tiket == null)
                        {
                            return NotFound();
                        }

                        return View(tiket);
                    }
                    if (tiket.WasUsed == false)
                    {
                        await _context.tickets
                            .Include(s => s.Entrance)
                            .FirstOrDefaultAsync(m => m.Id == id);

                        _context.Update(tiket);
                        await _context.SaveChangesAsync();
                        return View(tiket);
                    }
                }
            }
            return View(await _context.tickets.FindAsync(id));
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id < 1 || id > 5000)
            {
                ModelState.AddModelError(string.Empty, "Ese número de boleta no existe");
            }

            Ticket ticket = await _context.tickets.FindAsync(id);
            TicketViewModel model = new()
            {
                Id = ticket.Id,
                WasUsed = ticket.WasUsed,
                Name = ticket.Name,
                Document = ticket.Document,
                Date = ticket.Date,
                EntranceId = ticket.Entrance.Id,
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TicketViewModel model)
        {
            if (model.WasUsed = true)
            {
                return NotFound();
            }

            

            return View();
        }
        public async Task<IActionResult> AsignTicker()
        {
            Ticket ticket = await _context.tickets.FindAsync();
           

            TicketViewModel model = new()
            {
                Document = ticket.Document,
                Name = ticket.Name,
                Date = DateTime.Now,
                EntranceId = ticket.Entrance.Id,
               // Entrances = await _combosHelper.GetComboEntrancesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignTicker(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket tickect = await _context.tickets.FirstOrDefaultAsync();

                tickect.Document = model.Document;
                tickect.Name = model.Name;
                tickect.Date = model.Date;
                tickect.Entrance = await _context.entrances.FindAsync(model.EntranceId);

                _context.tickets.Update(tickect);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tickets");
            }

            //model.Entrances = await _combosHelper.GetComboEntrancesAsync();
            return View(model);
        }


        private bool TicketExists(int id)
        {
            return _context.tickets.Any(e => e.Id == id);
        }
    }
}
