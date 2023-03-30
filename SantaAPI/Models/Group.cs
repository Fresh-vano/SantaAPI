namespace SantaAPI.Models
{
    /// <summary>
    /// Модель группы
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание группы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Участники группы
        /// </summary>
        public List<int> Participants { get; set; }
    }
}
