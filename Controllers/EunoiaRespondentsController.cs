using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eunoia;
using System.IO;
using OfficeOpenXml;

namespace Eunoia.Controllers
{
    public class EunoiaRespondentsController : Controller
    {
        private readonly eunoiaContext _context;

        public EunoiaRespondentsController(eunoiaContext context)
        {
            _context = context;
        }

        // GET: EunoiaRespondents
        public async Task<IActionResult> Index()
        {
            return View(await _context.EunoiaRespondent.ToListAsync());
        }

        // GET: EunoiaRespondents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eunoiaRespondent = await _context.EunoiaRespondent
                .FirstOrDefaultAsync(m => m.RespondentId == id);
            if (eunoiaRespondent == null)
            {
                return NotFound();
            }

            return View(eunoiaRespondent);
        }

        // GET: EunoiaRespondents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EunoiaRespondents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RespondentId,RespondentFullName,RespondentAge,RespondentPassHash,RespondentEmail,OrganizationName,RespondentGender,RespondentDepartment,RespondentPosition")] EunoiaRespondent eunoiaRespondent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eunoiaRespondent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eunoiaRespondent);
        }

        // GET: EunoiaRespondents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eunoiaRespondent = await _context.EunoiaRespondent.FindAsync(id);
            if (eunoiaRespondent == null)
            {
                return NotFound();
            }
            return View(eunoiaRespondent);
        }

        // POST: EunoiaRespondents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RespondentId,RespondentFullName,RespondentAge,RespondentPassHash,RespondentEmail,OrganizationName,RespondentGender,RespondentDepartment,RespondentPosition")] EunoiaRespondent eunoiaRespondent)
        {
            if (id != eunoiaRespondent.RespondentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eunoiaRespondent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EunoiaRespondentExists(eunoiaRespondent.RespondentId))
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
            return View(eunoiaRespondent);
        }

        // GET: EunoiaRespondents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eunoiaRespondent = await _context.EunoiaRespondent
                .FirstOrDefaultAsync(m => m.RespondentId == id);
            if (eunoiaRespondent == null)
            {
                return NotFound();
            }

            return View(eunoiaRespondent);
        }

        // POST: EunoiaRespondents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var eunoiaRespondent = await _context.EunoiaRespondent.FindAsync(id);
            _context.EunoiaRespondent.Remove(eunoiaRespondent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EunoiaRespondentExists(string id)
        {
            return _context.EunoiaRespondent.Any(e => e.RespondentId == id);
        }

        public IActionResult Upload()
        { return View(); }

        [HttpPost]
        public void Upload(string file)
        {

            var fi = new FileInfo(@"C:\Desktop\" + file);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var ep = new ExcelPackage(fi))
            {

               var respondentSheet = ep.Workbook.Worksheets["Sample Data Set"];


                string valA1 = respondentSheet.Cells["B2"].Value.ToString();
            }
            
        }
    }
}
