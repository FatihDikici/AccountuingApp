using Microsoft.AspNetCore.SignalR;

namespace AccountingSystem.Api.Hubs;

public class AccountingHub : Hub
{
    // İstersen özel grup/oda mantığı ekleyebilirsin (müşteriId’ye göre vs.)
    // Şimdilik broadcast yeterli.
}
