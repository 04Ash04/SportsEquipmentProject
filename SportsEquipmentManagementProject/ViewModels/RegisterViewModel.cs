namespace SportsEquipmentManagementProject.ViewModels;

using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Имя пользователя обязательно.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    public string ConfirmPassword { get; set; }
}
