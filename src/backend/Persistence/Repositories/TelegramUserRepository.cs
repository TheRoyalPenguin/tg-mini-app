using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;
public class TelegramUserRepository : ITelegramUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TelegramUserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User?> GetByTelegramIdAsync(long telegramId)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.TgId == telegramId);
        return _mapper.Map<User>(userEntity);
    }

    public async Task AddAsync(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        await _context.Users.AddAsync(userEntity);
    }

    public async Task UpdateAsync(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        _context.Users.Update(userEntity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
