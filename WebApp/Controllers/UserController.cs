using BusinessLogicLayer.Interfaces;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult UserIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserIndex(UserViewModel userModel)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User
                {
                    Name = userModel.Name,
                    Birthdate = userModel.Birthdate,
                    Gender = userModel.Gender
                };

                bool isUserAdded = await _userService.AddUser(newUser);

                if(isUserAdded)
                {
                    TempData["SuccessMessage"] = "Usuario creado correctamente";
                    return RedirectToAction("QueryUser");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo agregar el usuario");
                }
            }
            return View(userModel);
        }

        public async Task<IActionResult> QueryUser()
        {
            SetViewBagMessages();

            List<User> users = await _userService.GetAllUsers();
            return View(users);
        }
        private void SetViewBagMessages()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            }
        }

        [HttpPost]
        public async Task<IActionResult> QueryUser(User editedUser)
        {
            User existingUser = await _userService.GetUserById(editedUser.UserId.ToString());

            if(existingUser == null)
            {
                TempData["ErrorMessage"] = "El usuario a modificar no existe en la base de datos";
                return RedirectToAction("QueryUser");
            }

            UserViewModel userModel = new UserViewModel
            {
                Name = editedUser.Name,
                Birthdate = editedUser.Birthdate,
                Gender = editedUser.Gender
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), validationResults, true);

            if (!isValid)
            {
                string errorMessage = "Error, verifique lo siguiente: ";
                foreach (var validationResult in validationResults)
                {
                    errorMessage += validationResult.ErrorMessage + " ";
                }
                // Delete end ,
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("QueryUser");
            }

            existingUser.Gender = editedUser.Gender;
            existingUser.Name = editedUser.Name;
            existingUser.Birthdate = editedUser.Birthdate;

            bool isUserUpdated = await _userService.UpdateUser(existingUser);

            if (isUserUpdated)
            {
                TempData["SuccessMessage"] = $"Los cambios al usuario '{existingUser.Name}' se aplicaron correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = $"No fue posible editar el usuario '{existingUser.Name}'";
            }

            return RedirectToAction("QueryUser");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                TempData["ErrorMessage"] = "Identificador de usuario inválido.";
                return RedirectToAction("QueryUser");
            }

            bool isUserDeleted = await _userService.DeleteUser(userId);

            if (isUserDeleted)
            {
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "No fue posible eliminar el usuario.";
            }

            return RedirectToAction("QueryUser");
        }
    }
}
