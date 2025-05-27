using MongoDB.Bson;
using MongoDB.Driver;
using Servers_service.Database;
using Servers_service.DTO;
using Servers_service.Models;
using System.Xml.Linq;

namespace Servers_service.Services
{
    public class ServerServices
    {
        private readonly MongoClient mongoClient = ServersDb.MongoClient();

        public async Task<dynamic> Post(ServerDTO serverDTO, int userId)
        {
            try
            {
                var database = mongoClient.GetDatabase("Servers");

                var server = new Server
                {
                    image = serverDTO.image,
                    name = serverDTO.name,
                    description = serverDTO.description,
                    link = $"https://discord/{serverDTO.name}/invitation",
                    creationDate = DateOnly.FromDateTime(DateTime.Now),
                    state = true,
                    creator = serverDTO.creator
                };

                var servers = database.GetCollection<Server>("Server");
                await servers.InsertOneAsync(server);

                var userServer = new UserServer
                {
                    userId = userId,
                    user = serverDTO.creator,
                    role = "admin",
                    serverId = server.id
                };

                var userServers = database.GetCollection<UserServer>("users_servers");
                await userServers.InsertOneAsync(userServer);

                return server;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<dynamic> Get(string name)
        {
            try
            {
                var database = mongoClient.GetDatabase("Servers");
                var collection = database.GetCollection<Server>("Server");
                var filter = Builders<Server>.Filter.Eq("name", name);
                var server = await collection.Find(filter).FirstOrDefaultAsync();
                if (server == null)
                {
                    return "no existe el servidor";
                }
                else
                {
                    return server;
                }
                    
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<dynamic> GetServers(string creator)
        {
            var database = mongoClient.GetDatabase("Servers");
            var collection = database.GetCollection<Server>("Server");
            var filter = Builders<Server>.Filter.Eq("creator", creator);
            var server = await collection.Find(filter).ToListAsync();
            if (server.Count == 0)
            {
                return "ese usuario no tiene servidores";
            } else
            {
                return server;
            }
        }

        public async Task<dynamic> GetUsers(string server)
        {
            var database = mongoClient.GetDatabase("Servers");
            var collection = database.GetCollection<Server>("Server");
            var filter = Builders<Server>.Filter.Eq("name", server);
            var data = await collection.Find(filter).FirstOrDefaultAsync();
            if (data == null)
            {
                return "el servidor no existe";
            }
            else
            {
                var userServersCollection = database.GetCollection<UserServer>("users_servers");
                var filter1 = Builders<UserServer>.Filter.Eq("serverId", data.id);
                var userServers = await userServersCollection.Find(filter1).ToListAsync();
                if (userServers.Count <= 1)
                {
                    return "el servidor no tiene usuarios";
                }
                else
                {
                    return userServers;
                }
            }
        }

        public async Task<string> Delete(int userId, string server)
        {
            try
            {
                var database = mongoClient.GetDatabase("Servers");
                var collection = database.GetCollection<Server>("Server");
                var filter = Builders<Server>.Filter.Eq("name", server);
                var data = await collection.Find(filter).FirstOrDefaultAsync();
                if (server == null)
                {
                    return "no existe el servidor";
                }
                else
                {
                    var userServerCollection = database.GetCollection<UserServer>("users_servers");
                    var filter1 = Builders<UserServer>.Filter.Eq("userId", userId);
                    var filter2 = Builders<UserServer>.Filter.Eq("serverId", data.id);
                    var filters = Builders<UserServer>.Filter.And(filter1, filter2);
                    var userServer = await userServerCollection.Find(filters).FirstOrDefaultAsync();
                    if (userServer.role == "admin")
                    {
                        await collection.DeleteOneAsync(filter);
                        await userServerCollection.DeleteManyAsync(filter2);
                        return "servidor eliminado con exito";
                    }
                    else
                    {
                        return "no tienes permisos para eliminar el servidor";
                    }
                }
            }
            catch (Exception ex)
            { 
                return ex.Message;
            }
        }

        public async Task<string> AddUserServer(int? id, string? user, string? server)
        {
            try
            {
                var database = mongoClient.GetDatabase("Servers");
                var collection = database.GetCollection<Server>("Server");
                var filter = Builders<Server>.Filter.Eq("name", server);
                var serverFound = await collection.Find(filter).FirstOrDefaultAsync();
                if (filter == null)
                {
                    return "no existe el servidor";
                }
                else
                {
                    var userServerCollection = database.GetCollection<UserServer>("users_servers");
                    var userServer = new UserServer
                    {
                        userId = id,
                        user = user,
                        role = "user",
                        serverId = serverFound.id
                    };
                    await userServerCollection.InsertOneAsync(userServer);
                    return "usuario agregado al servidor con exito";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteUserServer(int? id, string? user, string? server)
        {
            try
            {
                var database = mongoClient.GetDatabase("Servers");
                var collection = database.GetCollection<Server>("Server");
                var filter = Builders<Server>.Filter.Eq("name", server);
                var serverFound = await collection.Find(filter).FirstOrDefaultAsync();
                if (filter == null)
                {
                    return "no existe el servidor";
                }
                else
                {
                    var userServerCollection = database.GetCollection<UserServer>("users_servers");
                    var filter1 = Builders<UserServer>.Filter.Eq("userId", id);
                    var filter2 = Builders<UserServer>.Filter.Eq("serverId", serverFound.id);
                    var filters = Builders<UserServer>.Filter.And(filter1, filter2);
                    await userServerCollection.DeleteManyAsync(filters);
                    return "usuario eliminado del servidor con exito";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
