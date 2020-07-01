using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Translator.Data;
using Translator.Data.Models;

namespace Translator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly TranslatorApiContext _context;

        public TranslationController(TranslatorApiContext context)
        {
            _context = context;
        }

        // GET: api/Translation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translations>>> GetTranslations()
        {
            return await _context.Translations.Include(t => t.Languages)
                                              .Include(t => t.Tag).ToListAsync();
            
        }

        // GET: api/Translation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Translations>> GetTranslations(Guid id)
        {
            var translations = await _context.Translations.Include(t => t.Languages)
                                                          .Include(t => t.Tag).SingleOrDefaultAsync(t => t.Id == id);


            if (translations == null)
            {
                return NotFound();
            }

            return translations;
        }

        // PUT: api/Translation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslations(Guid id, Translations translations)
        {
            if (id != translations.Id)
            {
                return BadRequest();
            }

            _context.Entry(translations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Translation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Translations>> PostTranslations(Translations translations)
        {
            _context.Translations.Add(translations);
            await _context.SaveChangesAsync();
            var newTranslations =  _context.Translations.Include(t => t.Languages)
                                                        .Include(t => t.Tag).SingleOrDefault(t => t.Id == translations.Id);

            return CreatedAtAction("GetTranslations", new { id = translations.Id }, translations);
        }

        // DELETE: api/Translation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Translations>> DeleteTranslations(Guid id)
        {
            var translations = await _context.Translations.FindAsync(id);
            if (translations == null)
            {
                return NotFound();
            }

            _context.Translations.Remove(translations);
            await _context.SaveChangesAsync();

            return translations;
        }

        private bool TranslationsExists(Guid id)
        {
            return _context.Translations.Any(e => e.Id == id);
        }

        //GET : api/translation/fetch
        [HttpGet("fetch")]
        public async Task<ActionResult> FetchTagTableData()
        {

            var languages = await _context.Languages.ToListAsync();
            var tags = await _context.Tags.ToListAsync();

            var translationsList = await _context.Translations.Include(t => t.Languages)
                                 .Include(t => t.Tag).ToListAsync();


            return Ok(new {languages=languages,tags=tags, translationsList=translationsList});
        }

    }
}
