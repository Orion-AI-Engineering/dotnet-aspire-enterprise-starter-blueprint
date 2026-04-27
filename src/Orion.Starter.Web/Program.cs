using Orion.Starter.ServiceDefaults;
using Orion.Starter.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<OrderApiClient>(client =>
{
    client.BaseAddress = new("https+http://api");
});

var app = builder.Build();
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Orion Aspire Starter Web</title>
    <style>
        :root {
            color-scheme: light dark;
            font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
            background: #0f172a;
            color: #e5e7eb;
        }

        body {
            margin: 0;
            min-height: 100vh;
            display: grid;
            place-items: center;
            background:
                radial-gradient(circle at top left, rgba(59, 130, 246, 0.35), transparent 35%),
                radial-gradient(circle at bottom right, rgba(14, 165, 233, 0.25), transparent 30%),
                #0f172a;
        }

        main {
            max-width: 720px;
            margin: 2rem;
            padding: 3rem;
            border-radius: 1.5rem;
            background: rgba(15, 23, 42, 0.82);
            border: 1px solid rgba(148, 163, 184, 0.25);
            box-shadow: 0 24px 80px rgba(0, 0, 0, 0.35);
        }

        .eyebrow {
            margin-bottom: 1rem;
            color: #38bdf8;
            font-size: 0.875rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.12em;
        }

        h1 {
            margin: 0 0 1rem;
            font-size: clamp(2rem, 5vw, 3.75rem);
            line-height: 1;
            letter-spacing: -0.04em;
        }

        p {
            margin: 0;
            max-width: 620px;
            color: #cbd5e1;
            font-size: 1.15rem;
            line-height: 1.7;
        }

        .pill {
            display: inline-flex;
            margin-top: 2rem;
            padding: 0.7rem 1rem;
            border-radius: 999px;
            background: rgba(56, 189, 248, 0.12);
            color: #7dd3fc;
            font-weight: 600;
            border: 1px solid rgba(125, 211, 252, 0.24);
        }
    </style>
</head>
<body>
    <main>
        <div class="eyebrow">Orion AI Engineering</div>
        <h1>Orion Aspire Starter Web</h1>
        <p>
            This template deliberately keeps the UI very small so the architecture is easier to understand.
            The focus is on clean service composition, local development, observability, and production-ready
            distributed application patterns with Aspire 13 in .NET.
        </p>
        <div class="pill">Built with Microsoft's Aspire 13</div>
    </main>
</body>
</html>
""", "text/html"));

app.MapDefaultEndpoints();

app.Run();
