builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-keycloak-domain/auth/realms/your-realm";
        options.Audience = "account";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

// In UsersController.cs
private readonly IConnectionMultiplexer _redis;

public UsersController(LoyaltyContext context, IConnectionMultiplexer redis)
{
    _context = context;
    _redis = redis;
}

[HttpPost("{id}/earn")]
public async Task<IActionResult> EarnPoints(int id, [FromBody] EarnPointsRequest request)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null) return NotFound();

    user.Points += request.Points;
    await _context.SaveChangesAsync();

    var db = _redis.GetDatabase();
    await db.StringSetAsync($"user:{id}:points", user.Points);

    return Ok(user);
}
