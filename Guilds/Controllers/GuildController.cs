using Guilds.Models;
using Guilds.Models.DTO;
using Guilds.NetWorking;
using Microsoft.AspNetCore.Mvc;

namespace Guilds.Controllers
{
    /// <summary>
    /// Controller class for managing guild-related operations.
    /// </summary>
    public class GuildController : Controller
    {
        private readonly IBaseRepository<Guild> _guildRepo;
        private readonly IHttpApiRest _httpApiRest;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuildController"/> class.
        /// </summary>
        /// <param name="guildRepo">The repository for guild entities.</param>
        public GuildController(IBaseRepository<Guild> guildRepo, IHttpApiRest httpApiRest)
        {
            _guildRepo = guildRepo;
            _httpApiRest = httpApiRest;
        }

        /// <summary>
        /// Creates a new guild.
        /// </summary>
        /// <param name="guild">The data transfer object containing guild information.</param>
        /// <returns>The created guild information.</returns>
        [HttpPost]
        [Route("api/createguild")]
        public IActionResult CreateGuild([FromBody] CreateGuildDto guild)
        {
            var newGuild = guild.mapToGuild();

            _guildRepo.Add(newGuild);

            var joinedGuild = newGuild.mapToJoinedGuildDto();
            _httpApiRest.AddUserToGuild(newGuild.OwnerId, newGuild.Id);

            return Ok(joinedGuild);
        }

        /// <summary>
        /// Joins an existing guild.
        /// </summary>
        /// <param name="joinGuild">The data transfer object containing the guild to join.</param>
        /// <returns>The joined guild information.</returns>
        [HttpPost]
        [Route("api/joinguild")]
        public IActionResult JoinGuild([FromBody] JoinGuildDto joinGuild)
        {
            var guild = _guildRepo.Find(guild => guild.Id == joinGuild.Id).FirstOrDefault();

            if (guild == null)
            {
                return NotFound();
            }

            var newGuildUser = joinGuild.mapToGuildUser();

            guild.Users.ToList().Add(newGuildUser);

            _guildRepo.Update(guild);

            var joinedGuild = guild.mapToJoinedGuildDto();
            _httpApiRest.AddUserToGuild(newGuildUser.Id, guild.Id);

            return Ok(joinedGuild);
        }

        /// <summary>
        /// Retrieves a list of users in a guild.
        /// </summary>
        /// <param name="guildId">The ID of the guild.</param>
        /// <param name="skip">The number of users to skip.</param>
        /// <returns>The list of guild users.</returns>
        [HttpGet]
        [Route("api/getguilduser")]
        public IActionResult GetGuildUsers(Guid guildId, int skip)
        {
            var guild = _guildRepo.Find(guild => guild.Id == guildId).FirstOrDefault();

            if (guild == null)
            {
                return NotFound();
            }

            var guildUsers = guild.Users.Skip(skip).Take(50).ToArray();

            return Ok(guildUsers);
        }

        /// <summary>
        /// Deletes a user from a guild.
        /// </summary>
        /// <param name="guildId">The ID of the guild.</param>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>OK if the user was deleted successfully, NotFound otherwise.</returns>
        [HttpDelete]
        [Route("api/deleteguilduser")]
        public IActionResult DeleteGuildUser(Guid guildId, Guid userId)
        {
            var guild = _guildRepo.Find(guild => guild.Id == guildId).FirstOrDefault();

            if (guild == null)
            {
                return NotFound();
            }

            var guildUser = guild.Users.FirstOrDefault(user => user.Id == userId);

            if (guildUser == null)
            {
                return NotFound();
            }
            var userList = guild.Users.ToList();
            userList.Remove(guildUser);
            if (userList.Count == 0)
            {
                _guildRepo.Delete(guild);
                return Ok();
            }

            _httpApiRest.DeleteUserFromGuild(userId, guildId);

            _guildRepo.Update(guild);

            return Ok();
        }

        /// <summary>
        /// Deletes a guild.
        /// </summary>
        /// <param name="guildId">The ID of the guild to delete.</param>
        /// <returns>OK if the guild was deleted successfully, NotFound otherwise.</returns>
        [HttpDelete]
        [Route("api/deleteguild")]
        public IActionResult DeleteGuild(Guid guildId)
        {
            var guild = _guildRepo.Find(guild => guild.Id == guildId).FirstOrDefault();

            if (guild == null)
            {
                return NotFound();
            }
            ((List<GuildUser>)guild.Users).ForEach(user => _httpApiRest.DeleteUserFromGuild(user.Id, guildId));

            _guildRepo.Delete(guild);

            return Ok();
        }
        //Post request that creates a new channel in the given guild
        [HttpPost]
        [Route("api/createguildchannel")]
        public IActionResult CreateChannel([FromBody] CreateChannelDto channel)
        {
            var guild = _guildRepo.Find(guild => guild.Id == channel.GuildId).FirstOrDefault();

            if (guild == null)
            {
                return NotFound();
            }

            var newChannel = channel.mapToGuildChannel();

            ((List<GuildChannel>)guild.Channels).Add(newChannel);

            _guildRepo.Update(guild);

            Task.Run(() => {
                var usersId = guild.Users.Select(user => user.Id);
                _httpApiRest.CreateGuildTextChannel(guild.Id, usersId);
            });

            return Ok(newChannel.toJoinedChannelDto());
        }

        //Get all guilds
        [HttpGet]
        [Route("api/getallguilds")]
        public IActionResult GetAllGuilds()
        {
            var guilds = _guildRepo.GetAll();

            return Ok(guilds);
        }

        //Get all User guilds
        [HttpGet]
        [Route("api/getalluserguilds/{id}")]
        public async Task<IActionResult> GetAllUserGuildsAsync(Guid id)
        {
            var guildsId = await _httpApiRest.GetAllGuilds(id);

            var response = new GetAllUserGuildsResponseDto
            {
                Guilds = new List<Guild>(),
                Id = id
            };

            guildsId.ForEach(guildId => {
                var guild = _guildRepo.Find(guild => guild.Id == guildId).FirstOrDefault();
                if (guild != null)
                {
                    response.Guilds.Add(guild);
                }
            });

            return Ok(response);
        }
    }
}
