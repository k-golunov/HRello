using System.ComponentModel.DataAnnotations;
using Dal.User.Models;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities;

/// <summary>
/// Основная сущность, унаследованная от IdentityUser
/// </summary>
public class UserDal : IdentityUser
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [MaxLength(255)]
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [MaxLength(255)]
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [MaxLength(255)]
    public string Patronymic { get; set; }

    /// <summary>
    /// Поле, нужное для создания миграции через EF core
    /// Хранит отдел пользователя и все данные по нему
    /// </summary>
    public DepartamentDal Departament { get; set; }

    public override bool Equals(object? obj)
    {
        return Id == obj;
    }
}