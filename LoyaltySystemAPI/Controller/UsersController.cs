[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LoyaltyContext _context;

    public UsersController(LoyaltyContext context)
    {
        _context = context;
    }

    [HttpPost("{id}/earn")]
    public async Task<IActionResult> EarnPoints(int id, [FromBody] EarnPointsRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Points += request.Points;
        await _context.SaveChangesAsync();

        return Ok(user);
    }
}

public class EarnPointsRequest
{
    public int Points { get; set; }
}
