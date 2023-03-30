using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantaAPI.Context;
using SantaAPI.Models;

namespace SantaAPI.Controllers
{
    [Route("group/{id}/participant")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ParticipantController(ApplicationContext context)
        {
            _context = context;
        }

        private int NextId => _context.Peoples.Count() == 0 ? 1 : _context.Peoples.Max(x => x.Id) + 1;

        /// <summary>
        /// Добавление участника в группу по идентификатору группы
        /// </summary>
        /// <response code="200">Идентификатор добавленного учасника</response>
        /// <response code="400">Ошибка при добавлении учасника</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> PostPeople(int id, People people)
        {
            people.Id = NextId;
            _context.Peoples.Add(people);
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound("Такой группы нет!");
            }
            group.Participants.Add(people.Id);
            await _context.SaveChangesAsync();
            return Ok(people.Id);
        }

        /// <summary>
        /// Удаление участника из группы по идентификаторам
        /// </summary>
        /// <response code="200">Удаление учасника из группы</response>
        /// <response code="404">Группы или учасника не существует</response>
        /// <param name="id">Идентификатор группы</param>
        /// <param name="participantId">Идентификатор учасника</param>
        /// <returns></returns>
        [HttpDelete, Route("{participantId}")]
        public async Task<ActionResult> DeletePeople(int id, int participantId)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound("Такой группы нет!");
            }
            if (!group.Participants.Contains(participantId))
            {
                return NotFound("Такого учасника нет в группе");
            }
            group.Participants.Remove(participantId);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Получение информации для конкретного участника группы, кому он дарит подарок
        /// </summary>
        /// <response code="200">Информация об учаснике</response>
        /// <response code="404">Группы или учасника не существует</response>
        /// <param name="id">Идентификатор группы</param>
        /// <param name="participantId">Идентификатор учасника</param>
        /// <returns></returns>
        [HttpGet, Route("{participantId}/recipient")]
        public async Task<ActionResult<People>> GetRecipientPeople(int id, int participantId)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound("Такой группы нет!");
            }
            if (!group.Participants.Contains(participantId))
            {
                return NotFound("Такого учасника нет в группе");
            }
            var people = await _context.Peoples.FindAsync(participantId);
            if (people == null)
            {
                return NotFound("Такого учасника нет");
            }
            return Ok(people);
        }
    }
}
