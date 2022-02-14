namespace Blazr.Medusa.SPA;

public class MedusaService
{
    public string Site { get; set; } = "WASM";

    public bool IsWASM => this.Site.Equals("WASM");

    public bool IsServer => this.Site.Equals("Server");
}

