using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eunoia;
using System.IO;
using OfficeOpenXml;
using System.Text;

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
        public async Task<IActionResult> Upload(string file)
        {

            FileInfo fi = new FileInfo(Path.GetFullPath(file));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string[] gender = { "Female", "Male" };
            
            using (ExcelPackage ep = new ExcelPackage(fi))
            {
                ExcelWorksheet respondentSheet = ep.Workbook.Worksheets["Sample Data Set"];
                int totalRows = respondentSheet.Dimension.Rows;
                
                List<EunoiaRespondent> users = new List<EunoiaRespondent>();
                List<EunoiaAssessment> assess = new List<EunoiaAssessment>();

                for (int i = 2; i <= totalRows; i++)
                {
                    int gendernum = RandomNumber(0, 2);
                    StringBuilder assessid = new StringBuilder();
                    StringBuilder idbuild = new StringBuilder();
                    StringBuilder pass = new StringBuilder();
                    idbuild.Append(RandomString(3, true));
                    idbuild.Append(RandomNumber(100, 999));
                    pass.Append(RandomNumber(1000, 9999));
                    pass.Append(RandomString(5, true));
                    assessid.Append(RandomNumber(1000, 9999));

                    users.Add(new EunoiaRespondent
                    {
                        RespondentId = idbuild.ToString(),
                        RespondentFullName = respondentSheet.Cells[i, 2].Value.ToString(),
                        RespondentAge = respondentSheet.Cells[i, 3].Value.ToString(),
                        RespondentPassHash= pass.ToString(),
                        RespondentEmail=respondentSheet.Cells[i,4].Value.ToString(),
                        OrganizationName=respondentSheet.Cells[i,5].Value.ToString(),
                        RespondentGender=gender[gendernum],
                        RespondentDepartment="--",
                        RespondentPosition=respondentSheet.Cells[i,6].Value.ToString()
                    });
                    
                    assess.Add(new EunoiaAssessment
                    {
                        AssessmentId = Convert.ToInt32(assessid.ToString()),
                        RespondentId =idbuild.ToString(),
                        AssessmentDate=Convert.ToDateTime(respondentSheet.Cells[i,1].Value.ToString()),
                    }) ;
                       


                    
                }


                _context.EunoiaRespondent.AddRange(users);
                _context.EunoiaAssessment.AddRange(assess);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }




    }
}
