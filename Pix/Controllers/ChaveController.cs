using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pix.Context;
using Pix.DTO;
using Pix.Models;

namespace Pix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaveController
    {
        public readonly PixContext _context;
        public ChaveController(PixContext pixContext) {
            _context = pixContext;
        }

        [HttpGet]
        public List<Chave> Listar()
        {
            return _context.Chave.AsNoTracking()
                .Include(chv => chv.Cliente)
                .ToList();
        }

        [HttpGet("Cliente/{id}")]
        public List<Chave> ListarPorId(int id)
        {
            return _context.Chave.AsNoTracking()
                .Include(chv => chv.Cliente)
                .Where(chv => chv.ClienteId == id)
                .ToList();
        }

        [HttpPost]
        public void Create([FromBody] ChaveDTO chave)
        {
            Chave chavePix = new Chave();

            chavePix.ChavePix = chave.ChavePix;
            chavePix.ClienteId = chave.ClienteId;
            chavePix.Tipo = chave.Tipo;

            _context.Chave.Add(chavePix);
            _context.SaveChanges();

        }

        [HttpPut("{id}")]
        public void Update(ChaveDTO chave, int id)
        {
            var chaveAtual = _context.Chave.Find(id);

            try
            {
                if(chaveAtual is not null)
                {
                    if(!String.IsNullOrEmpty(chave.Tipo))
                        chaveAtual.Tipo = chave.Tipo;
                    if (!String.IsNullOrEmpty(chave.ChavePix))
                        chaveAtual.ChavePix = chave.ChavePix;

                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Vazio!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var chave = _context.Chave.Find(id);

            try
            {
                if( chave is not null)
                {
                    _context.Chave.Remove( chave );
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Chave Vazia!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
