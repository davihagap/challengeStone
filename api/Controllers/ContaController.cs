using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/contas/")]
    public class ContaController:ControllerBase
    {
        //get para obtenção de lista de todas as contas do banco
        //localhost:5000/contas
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Conta>>> Get([FromServices] ContaService service)
        {
            return await service.ListAsync();
        }

        //get para obtenção de uma conta especifica por número da conta
        //localhost:5000/contas/1
        [HttpGet]
        [Route("{num:int}")]
        public async Task<ActionResult<Conta>> GetByNum([FromServices] ContaService service, int num)
        {
            return await service.FindByNumAsync(num);
        }

        //get para obtenção do extrato da conta
        //localhost:5000/contas/1/extratos
        [HttpGet]
        [Route("{num:int}/extratos")]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetExtratos([FromServices] ContaService service, int num)
        {
            return await service.ListTransacoesAsync(num);
        }


        //Post para criação de conta
        //localhost:5000/contas
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Conta>> PostCriaConta(
            [FromServices] ContaService service,
            [FromBody] Conta model
        )
        {
            if (ModelState.IsValid)
            {                
                await service.SaveAsync(model);
                return Ok(model);
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de depósito em conta
        //localhost:5000/contas/3356/depositos/
        [HttpPost]
        [Route("{num:int}/depositos")]
        public async Task<ActionResult<TransacaoViewModel>> PostDeposito(
            [FromServices] ContaService service,
            [FromBody] TransacaoRequest model,
            int num
        )
        {
            if (ModelState.IsValid)
            {
                return await service.DepositarAsync(model);
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de saque em conta
        //localhost:5000/contas/3356/saques/
        [HttpPost]
        [Route("{num:int}/saques")]
        public async Task<ActionResult<TransacaoViewModel>> PostSaque(
            [FromServices] ContaService service,
            [FromBody] TransacaoRequest model,
            int num
        )
        {
            if (ModelState.IsValid)
            {
                return await service.SacarAsync(model);
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}