using Resend;

namespace HomeGrown.API.Services;

public interface IEmailService
{
    Task SendWaitlistConfirmationAsync(string toEmail);
    Task SendWelcomeAsync(string toEmail, string name, string role);
}

public class ResendEmailService(IResend resend, IConfiguration config, ILogger<ResendEmailService> logger)
    : IEmailService
{
    private string From => config["Resend:From"] ?? "HomeGrown <onboarding@resend.dev>";

    public async Task SendWaitlistConfirmationAsync(string toEmail)
    {
        var msg = new EmailMessage
        {
            From = From,
            Subject = "You're on the HomeGrown waitlist",
            HtmlBody = WaitlistHtml(),
        };
        msg.To.Add(toEmail);

        try
        {
            await resend.EmailSendAsync(msg);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send waitlist confirmation to {Email}", toEmail);
        }
    }

    public async Task SendWelcomeAsync(string toEmail, string name, string role)
    {
        var isFarmer = role.Equals("Farmer", StringComparison.OrdinalIgnoreCase);
        var msg = new EmailMessage
        {
            From = From,
            Subject = isFarmer
                ? "Welcome to HomeGrown — your farm profile is ready"
                : "Welcome to HomeGrown",
            HtmlBody = WelcomeHtml(name, isFarmer),
        };
        msg.To.Add(toEmail);

        try
        {
            await resend.EmailSendAsync(msg);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send welcome email to {Email}", toEmail);
        }
    }

    // ─── Templates ──────────────────────────────────────────────────────────

    private static string WaitlistHtml() => $"""
        {BaseHead()}
        <body style="margin:0;padding:0;background:#FAF7F2;font-family:'Helvetica Neue',Arial,sans-serif;">
          {Header()}
          <div style="max-width:560px;margin:0 auto;padding:40px 24px;">
            <h1 style="font-family:Georgia,serif;font-size:28px;font-weight:700;color:#1a1a1a;margin:0 0 12px;">
              You're on the list.
            </h1>
            <p style="font-size:16px;color:#6b6b6b;line-height:1.65;margin:0 0 24px;">
              Thanks for joining the HomeGrown waitlist. We'll reach out as soon as
              farms in your area are live on the platform.
            </p>
            <p style="font-size:15px;color:#6b6b6b;line-height:1.65;margin:0 0 32px;">
              In the meantime, you can browse farms that are already listed and
              subscribe to their seasonal boxes.
            </p>
            <a href="https://homegrown-web.vercel.app/farms"
               style="display:inline-block;background:#C4622D;color:#ffffff;text-decoration:none;
                      border-radius:50px;padding:14px 32px;font-size:15px;font-weight:600;">
              Browse Farms
            </a>
          </div>
          {Footer()}
        </body>
        """;

    private static string WelcomeHtml(string name, bool isFarmer) => $"""
        {BaseHead()}
        <body style="margin:0;padding:0;background:#FAF7F2;font-family:'Helvetica Neue',Arial,sans-serif;">
          {Header()}
          <div style="max-width:560px;margin:0 auto;padding:40px 24px;">
            <h1 style="font-family:Georgia,serif;font-size:28px;font-weight:700;color:#1a1a1a;margin:0 0 12px;">
              Welcome, {name}.
            </h1>
            {(isFarmer ? FarmerBody() : BuyerBody())}
          </div>
          {Footer()}
        </body>
        """;

    private static string FarmerBody() => """
        <p style="font-size:16px;color:#6b6b6b;line-height:1.65;margin:0 0 24px;">
          Your farmer account is ready. Head to your dashboard to set up your
          farm profile, add your growing practices, and create subscription plans
          for your customers.
        </p>
        <a href="https://homegrown-web.vercel.app/dashboard"
           style="display:inline-block;background:#2D5016;color:#ffffff;text-decoration:none;
                  border-radius:50px;padding:14px 32px;font-size:15px;font-weight:600;">
          Go to Dashboard
        </a>
        """;

    private static string BuyerBody() => """
        <p style="font-size:16px;color:#6b6b6b;line-height:1.65;margin:0 0 24px;">
          Your account is ready. Browse local farms, pick a subscription plan,
          and start getting fresh seasonal produce delivered straight from the
          farm to your door.
        </p>
        <a href="https://homegrown-web.vercel.app/farms"
           style="display:inline-block;background:#C4622D;color:#ffffff;text-decoration:none;
                  border-radius:50px;padding:14px 32px;font-size:15px;font-weight:600;">
          Browse Farms
        </a>
        """;

    private static string BaseHead() => """
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8"/>
          <meta name="viewport" content="width=device-width,initial-scale=1"/>
          <title>HomeGrown</title>
        </head>
        """;

    private static string Header() => """
        <div style="background:#2D5016;padding:24px;">
          <div style="max-width:560px;margin:0 auto;display:flex;align-items:center;gap:10px;">
            <span style="font-family:Georgia,serif;font-size:20px;font-weight:700;color:#FAF7F2;">
              HomeGrown
            </span>
          </div>
        </div>
        """;

    private static string Footer() => """
        <div style="max-width:560px;margin:0 auto;padding:32px 24px;border-top:1px solid #e8e2d9;">
          <p style="font-size:12px;color:#aaa;line-height:1.6;margin:0;">
            © 2026 HomeGrown. From the soil to your table.<br/>
            You're receiving this because you signed up at homegrown-web.vercel.app.
          </p>
        </div>
        """;
}
