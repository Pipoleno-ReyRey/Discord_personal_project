using Login_service.Database;
using Login_service.DTO;
using Login_service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Login_service.Services
{
    public class UserServices
    {
        private readonly UsersDb db;
        public UserServices(UsersDb db)
        {
            this.db = db;
        }

        private UserResponseDTO ProblemResponse(string ex)
        {
            return new UserResponseDTO
            {
                id = -1,
                name = "",
                photo = "",
                email = "",
                phone = "",
                password = "",
                confirm = false,
                message = ex
            };
        }

        public async Task<UserResponseDTO> Get(string data)
        {
            try
            {
                var user = await db.user.FirstOrDefaultAsync(u => u.email == data.ToLower() || u.name == data.ToLower());
                var userResult = new UserResponseDTO();
                if (user == null)
                {
                    userResult = ProblemResponse("usuario no encontrado");
                }
                else
                {
                    userResult = new UserResponseDTO
                    {
                        id = user.id,
                        name = user.name,
                        photo = user.photo,
                        email = user.email,
                        phone = user.phone,
                        password = user.password,
                        confirm = true,
                        message = "usuario encontrado"
                    };
                }

                return userResult;
            } catch(Exception ex)
            {
                return ProblemResponse(ex.Message);
            }
            
        }


        public async Task<UserResponseDTO> Post(UserDTO user)
        {
            try
            {
                var userDb = new User
                {
                    name = user.name!.ToLower(),
                    photo = user.photo,
                    email = user.email!.ToLower(),
                    phone = user.phone!,
                    password = user.password
                };

                if (await db.user.AnyAsync(u => u.email == userDb.email || u.name == userDb.name))
                {
                    return ProblemResponse("usuario ya existe");
                }
                else
                {
                    await db.user.AddAsync(userDb);
                    await db.SaveChangesAsync();

                    var userResponse = new UserResponseDTO
                    {
                        id = userDb.id,
                        name = userDb.name,
                        photo = userDb.photo,
                        email = userDb.email,
                        phone = userDb.phone,
                        password = userDb.password,
                        confirm = true,
                        message = "usuario agregado"
                    };

                    return userResponse;
                }
                    
            }catch(Exception ex)
            {
                return ProblemResponse(ex.Message);
            }
        }

        public async Task<UserResponseDTO> Update(User user)
        {
            try
            {
                var userDb = await db.user.FirstOrDefaultAsync(u => u.id == user.id);
                userDb!.name = user.name?.ToLower();
                userDb.photo = user.photo;
                userDb.email = user.email?.ToLower();
                userDb.phone = user.phone;
                userDb.password = user.password;
                await db.SaveChangesAsync();

                var userResponse = new UserResponseDTO
                {
                    id = userDb.id,
                    name = userDb.name,
                    photo = userDb.photo,
                    email = userDb.email,
                    phone = userDb.phone,
                    password = userDb.password,
                    confirm = true,
                    message = "usuario actualizado"
                };

                return userResponse;
            }
            catch(Exception ex)
            {
                return ProblemResponse(ex.Message);
            }
        }

        public async Task<UserResponseDTO> Delete(int id)
        {
            try
            {
                var userDb = await db.user.FirstOrDefaultAsync(u => u.id == id);
                if (userDb == null)
                {
                    return ProblemResponse("usuario no encontrado");
                }
                else
                {
                    db.user.Remove(userDb);
                    var userResponse = new UserResponseDTO
                    {
                        id = userDb.id,
                        name = userDb.name,
                        photo = userDb.photo,
                        email = userDb.email,
                        phone = userDb.phone,
                        password = userDb.password,
                        confirm = true,
                        message = "usuario eliminado"
                    };
                    await db.SaveChangesAsync();
                    return userResponse;
                }
              
            }catch(Exception ex)
            {
                return ProblemResponse(ex.Message);
            }
        }
    }
}
