using Microsoft.AspNetCore.Mvc;
using  BasicAPI.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public FornecedoresController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorModel>>> GetFornecedores()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorModel>> GetFornecedor(Guid id)
        {
            var fornecedor =  await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null)
            {

                return NotFound();
            }

            return fornecedor; 
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutFornecedor (Guid id, FornecedorModel fornecedor)
        {
            if (id != fornecedor.Id){
                return BadRequest(); 
            }

            _context.Entry(fornecedor).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (! await FornecedorExists(id))
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

        [HttpPost]
        public async Task<ActionResult<FornecedorModel>> PostFornecedor(FornecedorModel fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);

            await _context.SaveChangesAsync(); 

            return CreatedAtAction("GetFornecedor", new {id = fornecedor.Id}, fornecedor); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FornecedorModel>> DeleteFornecedor(Guid id){
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null){
                return NotFound(); 
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return fornecedor;

        }

        private Task<Boolean>FornecedorExists(Guid id)
        {
            return _context.Fornecedores.AnyAsync(e => e.Id == id);
        }
    }
}