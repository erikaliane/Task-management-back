using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.Models;
using TaskManagementAPI.Utils;

namespace TaskManagementAPI.Services;

public class AuthService
{
    private readonly TaskManagementDbContext _context;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthService(TaskManagementDbContext context, JwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            return null; 
        }

        
        return _tokenGenerator.GenerateToken(user.UserId.ToString(), user.Role);
    }
}