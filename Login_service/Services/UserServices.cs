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

        public async Task<dynamic> Get(string data)
        {
            try
            {
                var user = await db.user.FirstOrDefaultAsync(u => u.email == data.ToLower() || u.name == data.ToLower());
                if (user == null)
                {
                    return "usuario no encontrado";
                }
                else
                {
                    var userResult = new UserResponseDTO
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

                    return userResult;
                }

            } catch(Exception ex)
            {
                return ex.Message;
            }
            
        }


        public async Task<dynamic> Post(UserDTO user)
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
                    return "el usuario ya existe";
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
                return ex.Message;
            }
        }

        public async Task<dynamic> Update(User user)
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
                return ex.Message;
            }
        }

        public async Task<dynamic> Delete(int id)
        {
            try
            {
                var userDb = await db.user.FirstOrDefaultAsync(u => u.id == id);
                if (userDb == null)
                {
                    return "usuario no encontrado";
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
                return ex.Message;
            }
        }
    }
}
