using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class AdminContext(DbContextOptions<AdminContext> options) : IdentityDbContext<IdentityUser>(options);