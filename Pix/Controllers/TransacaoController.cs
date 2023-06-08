using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pix.Context;
using Pix.DTO;
using Pix.Models;

namespace Pix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransacaoController
    {
        public readonly PixContext _context;
        public TransacaoController(PixContext pixContext)
        { 
            _context = pixContext;
        }

        [HttpGet]
        public List<Transacao> Listar()
        {
            return _context.Transacao.AsNoTracking()
                .Include(tran => tran.Cliente)
                .Include(tran => tran.Chave)
                .ToList();
        }

        [HttpGet("Cliente/{id}")]
        public List<Transacao> ListarPorId(int id)
        {
            return _context.Transacao.AsNoTracking()
                .Include(tran => tran.Cliente)
                .Where(tran => tran.ClienteId == id)
                .ToList();
        }

        [HttpPost]
        public void Create([FromBody] Transacao transacao)
        {
            try
            {
                Transacao transacoes = new Transacao();

                transacoes.Nome = transacao.Nome;
                transacoes.DataHora = transacao.DataHora;
                transacoes.Valor = ((double)transacao.Valor);
                transacoes.ClienteId = transacao.ClienteId;
                transacoes.ChaveId = transacao.ChaveId;

                _context.Transacao.Add(transacoes);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        [HttpPut("{id}")]
        public void Update(Transacao transacao, int id)
        {
            try
            {
                var transacaoAtual = _context.Transacao.Find(id);

                if (transacaoAtual is not null)
                {
                    if(!String.IsNullOrEmpty(transacao.Nome))
                        transacaoAtual.Nome = transacao.Nome;
                    if (transacao.DataHora != DateTime.MinValue)
                        transacaoAtual.DataHora = transacao.DataHora;
                    if(transacao.Valor != 0)
                        transacaoAtual.Valor = transacao.Valor;

                    _context.SaveChanges();
                }
                else
                {
                    return;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                var transacao = _context.Transacao.Find(id);

                if(transacao is not null)
                { 
                    _context.Transacao.Remove(transacao);
                    _context.SaveChanges();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() );
            }
        }
    }
}
