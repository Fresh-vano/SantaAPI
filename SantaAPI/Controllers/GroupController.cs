using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaAPI.Context;
using SantaAPI.Models;

namespace SantaAPI.Controllers
{
    [Route("group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public GroupController(ApplicationContext context)
        {
            _context = context;
        }

        private int NextId => _context.Groups.Count() == 0 ? 1 : _context.Groups.Max(x => x.Id) + 1;

        /// <summary>
        /// Добавление группы с возможностью указания названия (name), описания (description)
        /// </summary>
        /// <param name="group">Добавляемая группа</param>
        /// <response code="200">Идентификатор созданной группы</response>
        /// <response code="400">Ошибка при создании группы</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> PostGroup(Group group)
        {
            group.Id = NextId;
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return Ok(group.Id);
        }

        /// <summary>
        /// Получение краткой информации о всех группах
        /// </summary>
        /// <response code="200">Список всех групп</response>
        /// <response code="400">Ошибка при получении списка групп</response>
        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetGroup()
        {
            return Ok(await _context.Groups.ToListAsync());
        }

        /// <summary>
        /// Получение информации о группе
        /// </summary>
        /// <response code="200">Требуемая группа</response>
        /// <response code="400">Требуемой группы нет</response>
        /// <param name="id">Идентификатор группы</param>
        [HttpGet, Route("{id}")]
        public async Task<ActionResult<Group>> GetSingleGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound("Такой группы нет!");
            }
            return Ok(group);
        }

        /// <summary>
        /// Редактирование группы по идентификатору группы
        /// </summary>
        /// <response code="200">Группа отредактированна</response>
        /// <response code="400">Передаваемое значение имени не должно быть пустым</response>
        /// <response code="404">Группы с таким id нет</response>
        /// <param name="id">Идентификатор группы</param>
        /// <param name="group">Редактируемые свойства</param>
        [HttpPut, Route("{id}")]
        public async Task<ActionResult> EditGroup(int id, Group group)
        {
            var editGroup = await _context.Groups.FindAsync(id);
            if (editGroup == null)
                return NotFound("Группы с таким id нет!");
            if(string.IsNullOrEmpty(group.Name))
                return BadRequest("Имя не должно быть пустым!");
            editGroup.Name = group.Name;
            editGroup.Description = group.Description;
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Удаление группы по идентификатору
        /// </summary>
        /// <response code="200">Требуемая группа удалена</response>
        /// <response code="404">Группы с таким id нет</response>
        /// <param name="id">Идентификатор группы</param>
        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                NotFound("Группы с таким id нет!");
            }
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return Ok();
        }

        ///// <summary>
        ///// Проведение жеребьевки в группе по идентификатору группы
        ///// </summary>
        ///// <response code="200">Требуемая группа удалена</response>
        ///// <response code="404">Группы с таким id нет</response>
        ///// <param name="id">Идентификатор группы</param>
        //[HttpPost, Route("{id}/toss")]
        //public async Task<ActionResult<List<People>>> TossGroup(int id)
        //{
        //    var group = await _context.Groups.FindAsync(id);
        //    if (group == null)
        //    {
        //        NotFound("Группы с таким id нет!");
        //    }
        //    if (group.Participants.Count < 3)
        //    {
        //        return Conflict();
        //    }
        //    for (int i = 0; i < group.Participants.Count; i++)
        //    {

        //    }
            
        //    await _context.SaveChangesAsync();
        //    return Ok(group.Participants);
        //}
    }
}
