using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("contas/")]
    public class ContaController:ControllerBase
    {
        //get para obtenção de lista de todas as contas do banco
        //localhost:5000/contas
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Conta>>> Get([FromServices] DataContext context)
        {
            var contas = await context.Contas.ToListAsync();
            return contas;
        }

        //get para obtenção de uma conta especifica por número da conta
        //localhost:5000/contas/1
        [HttpGet]
        [Route("{num:int}")]
        public async Task<ActionResult<Conta>> GetByNum([FromServices] DataContext context, int num)
        {
            var conta = await context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync( c => c.Numero == num);
            if (conta != null)
                return conta;
            else
                return NotFound("Conta inexistente");
        }


        //Post para criação de conta
        //localhost:5000/contas
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Conta>> Post(
            [FromServices] DataContext context,
            [FromBody] Conta model
        )
        {
            if (ModelState.IsValid)
            {
                context.Contas.Add(model);
                await context.SaveChangesAsync();
                return model;
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de depósito em conta
        //localhost:5000/contas/3356/depositos/
        [HttpPost]
        [Route("deposito/{num:int}/depositos")]
        public async Task<ActionResult<Conta>> Post(
            [FromServices] DataContext context,
            [FromBody] Transacao model,
            int num
        )
        {
            return NotFound("Ainda em desenvolvimento");
        }
    }
}