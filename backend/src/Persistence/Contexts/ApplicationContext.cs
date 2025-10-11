using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Users;

namespace Persistence.Contexts;

public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<User>(options)
{
}