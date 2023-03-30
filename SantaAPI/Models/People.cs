namespace SantaAPI.Models
{
    /// <summary>
    /// Модель участника
    /// </summary>
    public class People
    {
        /// <summary>
        /// Идентификатор участника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя участника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пожелание
        /// </summary>
        public string Wish { get; set; } = string.Empty;

        /// <summary>
        /// Подопечный
        /// </summary>
        public int Recipient { get; set; }
    }
}
