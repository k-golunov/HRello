using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;

namespace Dal.Entities;

/// <summary>
/// Сущность для отделов в компании
/// Нужна для разделения сотрудников на группы меньше
/// </summary>
public class DepartamentDal : BaseDal<int>
{
    /// <summary>
    /// Название отдела
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    /// <summary>
    /// Уникальный идентификатор начальника отдела
    /// </summary>
    public string BossId { get; set; }
}