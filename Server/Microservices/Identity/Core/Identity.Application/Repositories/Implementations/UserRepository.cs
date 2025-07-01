using Identity.Application.DTOs.User;
using Identity.Application.Interfaces;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Application.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(IApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Remove(ICollection<string> emails)
        {
            foreach (string email in emails)
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

                await _userManager.DeleteAsync(user);
            }
        }
        public async Task Update(Expression<Func<User, bool>> predicate, Action<User> update)
        {
            User user = await _context.Users.FirstOrDefaultAsync(predicate);

            update(user);

            await _userManager.UpdateAsync(user);
        }

        public async Task<UserGetDTO> Get(Expression<Func<User, bool>> predicate)
        {
            User user = await _context.Users.FirstOrDefaultAsync(predicate);

            UserGetDTO dto = new UserGetDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
            };

            return dto;
        }

        public async Task<ICollection<UserGetDTO>> GetAll(Guid? id = null)
        {
            IQueryable<User> query = _context.Users;

            if (id.HasValue)
                query = query.Where(x => x.Id != id);

            List<User> users = await query.ToListAsync();

            List<UserGetDTO> dtos = new List<UserGetDTO>();
            foreach (User user in users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                UserGetDTO dto = new UserGetDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
