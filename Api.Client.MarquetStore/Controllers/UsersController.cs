using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Org.BouncyCastle.Bcpg;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

    }
}
