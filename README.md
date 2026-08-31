# VolSurface.API

Start with `VolSurface.API/Controllers/VolSurfaceController.cs`, then work outward through
`Services/` and `Models/`. Read the whole thing, not just the controller. Review it as if
it's heading to production: for anything you'd flag in a real code review - correctness,
design, anything else - note what's wrong and how you'd fix it.

`VolSurface.API/Ignore/` stands in for a real database and a real upstream market data
endpoint. It's test scaffolding, not part of what you're reviewing - leave it alone.

## Running it

```
cd VolSurface.API
dotnet run
```

Swagger UI is at `/swagger` once it's running.
