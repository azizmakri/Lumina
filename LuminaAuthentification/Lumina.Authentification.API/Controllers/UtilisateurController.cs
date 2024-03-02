using Lumina.Authentification.Application.UtilisateurFeature;
using Lumina.Authentification.Application.UtilisateurFeature.Commands;
using Lumina.Authentification.Application.UtilisateurFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lumina.Authentification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UtilisateurController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/<UtilisateurController>
        [HttpGet]
        public async Task<List<UtilisateurDto>> Get()
        {
            var utilisateurs = await _mediator.Send(new GetUtilisateursQuery());
            return utilisateurs;
        }

        // GET api/<UtilisateurController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UtilisateurController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(CreateUtilisateurCommand utilisateur)
        {
            var response = await _mediator.Send(utilisateur);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<UtilisateurController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateUtilisateurCommand utilisateur)
        {
            await _mediator.Send(utilisateur);
            return NoContent();
        }

        // DELETE api/<UtilisateurController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var Command = new DeleteUtilisateurCommand {Identifiant=id };
            await _mediator.Send(Command);
            return NoContent();
        }
    }
}
