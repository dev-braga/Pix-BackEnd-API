using Microsoft.AspNetCore.Mvc;
using Pix.Context;
using Pix.Models;

namespace Pix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController
    {
        public readonly PixContext _context;
        public ClienteController( PixContext pixContext)
        {
            _context = pixContext;
        }

        [HttpGet]
        public List<Cliente> Listar()
        {
            return _context.Cliente.ToList();
        }

        [HttpPost]
        public void Create([FromBody] Cliente cliente)
        {
           Cliente clienteEntity = new Cliente();
            
            clienteEntity.Nome = cliente.Nome;
            clienteEntity.Email = cliente.Email;

            _context.Cliente.Add(clienteEntity);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Update([FromBody] Cliente cliente, int id)
        {
            var clienteAtual = _context.Cliente.Find(id);
            
            if( clienteAtual is not null)
            {
                if (!String.IsNullOrEmpty(cliente.Email))
                    clienteAtual.Email = cliente.Email;
                if (!String.IsNullOrEmpty(cliente.Nome))
                    clienteAtual.Nome = cliente.Nome;

                _context.SaveChanges(); 
            }
            else
            {
                Console.WriteLine(Console.Error); 
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var clienteEntity = _context.Cliente.Find(id);

            try
            {
                if(clienteEntity is not null )
                {
                    _context.Cliente.Remove(clienteEntity);
                    _context.SaveChanges();
                }
                else 
                { 
                    Console.WriteLine(Console.Error);
                }  
            }
            catch (Exception ex){ 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
