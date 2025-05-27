using Channels_service.Database;
using Channels_service.DTO;
using Channels_service.Models;
using MongoDB.Driver;

namespace Channels_service.Services
{
    public class ServicesChannels
    {
        private IMongoDatabase db = new ChannelsDB().database;

        public async Task<dynamic> Post(ChannelDTO channelDTO, string serverId, string server)
        {
            try
            {
                var channel = new Channel
                {
                    name = channelDTO.name,
                    description = channelDTO.description,
                    serverId = serverId,
                    server = server
                };
                var collection = db.GetCollection<Channel>("channels");
                await collection.InsertOneAsync(channel);
                return channel;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<dynamic> GetAll(string serverId)
        {
            try
            {
                var collection = db.GetCollection<Channel>("channels");
                var filter = Builders<Channel>.Filter.Eq("serverId", serverId);
                return await collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<dynamic> Delete(string channel, string serverId, string role)
        {
            try
            {
                if (role != "admin")
                {
                    return "no tienes permisos para eliminar un canal";
                }
                else
                {
                    var collection = db.GetCollection<Channel>("channels");
                    var filter = Builders<Channel>.Filter.And(
                        Builders<Channel>.Filter.Eq("serverId", serverId),
                        Builders<Channel>.Filter.Eq("name", channel)
                    );
                    var result = await collection.DeleteOneAsync(filter);

                    return "canal borrado";
                }
                    
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
